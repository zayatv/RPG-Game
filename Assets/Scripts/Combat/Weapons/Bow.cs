using Animancer;
using RPG.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.Combat
{
    public class Bow : WeaponBehavior
    {
        [SerializeField] protected Arrow arrowPrefab;
        [SerializeField] protected Transform arrowSpawnPoint;

        [Header("Standard Combo")]
        [SerializeField] protected AttackMotion[] attackCombo;
        [SerializeField] protected float comboCooldown = 0.3f; //How long after the last attack before the combo can be started again

        [Header("Charge")]
        [SerializeField] protected float maxHoldTime = 2f;
        [SerializeField] protected bool autoReleaseAferMax = false; //Attack automatically when fully charged instead of waiting for user to release input
        [SerializeField] protected OnHitEffect chargeHitEffect;
        [SerializeField] protected ClipTransition chargeStartAnim;
        [SerializeField] protected ClipTransition chargeLoopAnim;
        [SerializeField] protected ClipTransition chargeAttackAnim;

        [Header("Specials")]
        [SerializeField] protected bool allowSprintAttack = true;
        [SerializeField, ShowIf(nameof(allowSprintAttack))] protected AttackMotion sprintAttack;

        private Movement movement;
        private PlayerTargeting targeting;
        private Transform currentTarget;

        private AnimancerState currentComboState;
        private float lastAttackEndTime;
        private bool comboQueued;
        private int currentAttackIndex = 0;

        private bool specialAttacking;
        private bool chargingAttack;
        private float chargeStartTime;
        private float chargeEndTime;

        protected override void Start()
        {
            base.Start();
            movement = user.GetComponent<Movement>();
            targeting = user.GetComponent<PlayerTargeting>();

            chargeStartAnim.Events.OnEnd += BeginChargeLoop;
            sprintAttack.animation.Events.OnEnd += OnSprintAttackEnd;
            chargeAttackAnim.Events.OnEnd += OnChargeAttackEnd;
        }

        protected override void Update()
        {
            base.Update();

            if (autoReleaseAferMax)
            {
                if (chargingAttack && !specialAttacking && Time.time - chargeStartTime > maxHoldTime)
                    ChargeInput_Release(new InputAction.CallbackContext());
            }
        }

        protected override void AttackInput_Performed(InputAction.CallbackContext context)
        {
            base.AttackInput_Performed(context);

            //Ignore input if airborne. Alternatively add logic here for air attacks.
            if (!user.Motor.GroundingStatus.IsStableOnGround)
                return;

            if (currentComboState != null)
            {
                //Queue the next attack if already in an attack
                comboQueued = true;
            }
            else
            {
                if (movement.MoveState == MoveState.Sprinting && allowSprintAttack)
                {
                    SprintAttack();
                }
                else if (Time.time - lastAttackEndTime > comboCooldown && !specialAttacking)
                {
                    ComboAttack();
                }
            }
        }

        protected override void ChargeInput_Start(InputAction.CallbackContext context)
        {
            base.ChargeInput_Start(context);

            if (!user.Motor.GroundingStatus.IsStableOnGround)
                return;

            //Can't charge while doing another attack
            if (currentComboState != null || specialAttacking || comboQueued)
                return;

            if (chargingAttack)
                return;

            chargingAttack = true;
            user.CurrentAnimator.Play(chargeStartAnim);
            user.CurrentAnimator.applyRootMotion = true;
            chargeStartTime = Time.time;

            targeting.StartAiming();
        }

        protected override void ChargeInput_Release(InputAction.CallbackContext context)
        {
            base.ChargeInput_Release(context);

            if (chargingAttack && !specialAttacking)
            {
                specialAttacking = true;
                user.CurrentAnimator.Play(chargeAttackAnim);
                chargeEndTime = Time.time;
                targeting.StopAiming();
            }
        }

        private void BeginChargeLoop()
        {
            user.CurrentAnimator.Play(chargeLoopAnim);
        }

        private void OnChargeAttackEnd()
        {
            user.CurrentAnimator.PlayController();
            user.CurrentAnimator.applyRootMotion = false;
            currentAttackIndex = 0;
            specialAttacking = false;
            chargingAttack = false;
        }

        protected virtual void ComboAttack()
        {
            var anim = attackCombo[currentAttackIndex].animation;
            anim.Events.OnEnd += OnComboAttackEnd;
            currentComboState = user.CurrentAnimator.Play(anim);

            user.CurrentAnimator.applyRootMotion = attackCombo[currentAttackIndex].rootMotion;

            currentTarget = targeting.FindTarget(25f);

            if (currentTarget != null)
            {
                var dir = (currentTarget.transform.position - user.transform.position).normalized;
                movement.OverrideLookDir = dir;
            }
        }

        private void OnComboAttackEnd()
        {
            var anim = attackCombo[currentAttackIndex].animation;
            anim.Events.OnEnd -= OnComboAttackEnd;
            currentComboState = null;
            lastAttackEndTime = Time.time;
            movement.OverrideLookDir = Vector3.zero;
            currentTarget = null;

            if (comboQueued && currentAttackIndex + 1 < attackCombo.Length)
            {
                //Continue on to the next attack in the combo
                comboQueued = false;
                currentAttackIndex++;
                ComboAttack();
            }
            else
            {
                //Combo was dropped or completed the last attack, go back to animator.
                comboQueued = false;
                currentAttackIndex = 0;
                user.CurrentAnimator.PlayController();
                user.CurrentAnimator.applyRootMotion = false;
            }
        }

        protected virtual void SprintAttack()
        {
            specialAttacking = true;
            user.CurrentAnimator.Play(sprintAttack.animation);
            user.CurrentAnimator.applyRootMotion = sprintAttack.rootMotion;

            currentTarget = targeting.FindTarget(25f);

            if (currentTarget != null)
            {
                var dir = (currentTarget.transform.position - user.transform.position).normalized;
                movement.OverrideLookDir = dir;
            }
        }

        private void OnSprintAttackEnd()
        {
            specialAttacking = false;
            user.CurrentAnimator.PlayController();
            user.CurrentAnimator.applyRootMotion = false;
            currentAttackIndex = 0;
            movement.OverrideLookDir = Vector3.zero;
            currentTarget = null;
        }

        //Call from animation events on each attack animation
        public void ShootArrow()
        {
            var rotation = arrowSpawnPoint.rotation;

            if (currentTarget != null)
            {
                //Fire at target's chest instead if there is a target
                var targetPos = currentTarget.position + (currentTarget.up * 1.2f);
                var dir = (targetPos - arrowSpawnPoint.position).normalized;
                rotation = Quaternion.LookRotation(dir);
            }
            else if (chargingAttack)
            {
                //Fire at the point aimed at while charging.
                //Could also replace standard arrow with a different charged arrow here
                var aimDir = (targeting.AimTarget.position - arrowSpawnPoint.position).normalized;
                rotation = Quaternion.LookRotation(aimDir);
            }

            var arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, rotation);

            if (chargingAttack)
                arrow.OnHit = chargeHitEffect;
            else if (specialAttacking)
                arrow.OnHit = sprintAttack.onHitEffect;
            else
                arrow.OnHit = attackCombo[currentAttackIndex].onHitEffect;

            arrow.Shoot();
        }
    }
}