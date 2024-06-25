using Animancer;
using RPG.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Combat
{
    public class MeleeWeapon : WeaponBehavior
    {
        [Header("Standard Combo")]
        [SerializeField] protected MeleeAttack[] attackCombo;
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
        [SerializeField, ShowIf(nameof(allowSprintAttack))] protected MeleeAttack sprintAttack;
        [SerializeField] protected bool allowDashAttack = true;
        [SerializeField, ShowIf(nameof(allowDashAttack))] protected MeleeAttack dashAttack;
        [SerializeField, ShowIf(nameof(allowDashAttack))] protected bool attackAfterDash = true; //Queue the attack to play after the dash, interrupt dash if false.

        private Movement movement;
        
        private AnimancerState currentComboState;
        private float lastAttackEndTime;
        private bool comboQueued;
        private int currentAttackIndex = 0;

        private bool dashAttackQueued;
        private bool specialAttacking;

        private bool chargingAttack;
        private float chargeStartTime;
        private float chargeEndTime;

        protected override void Start()
        {
            base.Start();
            movement = user.GetComponent<Movement>();

            chargeStartAnim.Events.OnEnd += BeginChargeLoop;
            dashAttack.animation.Events.OnEnd += OnDashAttackEnd;
            sprintAttack.animation.Events.OnEnd += OnSprintAttackEnd;
        }

        protected override void Update()
        {
            base.Update();

            if (dashAttackQueued)
            {
                if (!movement.isDashing)
                {
                    //Wait for the dash to finish and then attack
                    dashAttackQueued = false;
                    DashAttack();
                }
            }

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
                if (movement.isDashing && allowDashAttack)
                {
                    if (attackAfterDash)
                        dashAttackQueued = true;
                    else
                        DashAttack();
                }
                else if (movement.MoveState == MoveState.Sprinting && allowSprintAttack)
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
        }

        protected override void ChargeInput_Release(InputAction.CallbackContext context)
        {
            base.ChargeInput_Release(context);

            if (chargingAttack && !specialAttacking)
            {
                specialAttacking = true;
                user.CurrentAnimator.Play(chargeAttackAnim);
                chargeAttackAnim.Events.OnEnd += OnChargeAttackEnd;
                chargeEndTime = Time.time;
            }
        }

        private void BeginChargeLoop()
        {
            user.CurrentAnimator.Play(chargeLoopAnim);
        }

        private void OnChargeAttackEnd()
        {
            chargeAttackAnim.Events.OnEnd -= OnChargeAttackEnd;
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
        }

        private void OnComboAttackEnd()
        {
            var anim = attackCombo[currentAttackIndex].animation;
            anim.Events.OnEnd -= OnComboAttackEnd;
            currentComboState = null;
            lastAttackEndTime = Time.time;

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

        protected virtual void DashAttack()
        {
            specialAttacking = true;
            user.CurrentAnimator.Play(dashAttack.animation);
            user.CurrentAnimator.applyRootMotion = dashAttack.rootMotion;
        }

        private void OnDashAttackEnd()
        {
            specialAttacking = false;
            user.CurrentAnimator.PlayController();
            user.CurrentAnimator.applyRootMotion = false;
            currentAttackIndex = 0;
        }

        protected virtual void SprintAttack()
        {
            specialAttacking = true;
            user.CurrentAnimator.Play(sprintAttack.animation);
            user.CurrentAnimator.applyRootMotion = sprintAttack.rootMotion;
        }

        private void OnSprintAttackEnd()
        {
            specialAttacking = false;
            user.CurrentAnimator.PlayController();
            user.CurrentAnimator.applyRootMotion = false;
            currentAttackIndex = 0;
        }
    }
}