using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem.Movesets
{
    public class BowAimAttack : MovesetComponent
    {
        public float maxHoldTime = 2f;
        [Tooltip("The Animator must be in one of the states having these tags to allow attacking.")]
        public List<string> validStates = new List<string> { "Grounded" };
        public OnHitEffect onHitEffect;

        public override IMovesetBehavior GetBehavior()
        {
            return new BowAimBehavior(this);
        }
    }

    public class BowAimBehavior : MovesetBehavior<BowAimAttack>
    {
        private Animator animator;
        private Armory armory;
        private bool charging;
        private float chargeStartTime;
        private float chargeEndTime;
        private Player player;
        private Transform projectile;
        private List<Transform> projectiles;

        public BowAimBehavior(BowAimAttack data) : base(data)
        {

        }

        public override void Equip(GameObject user)
        {
            base.Equip(user);

            animator = user.GetComponent<Animator>();
            armory = user.GetComponent<Armory>();
            player = Player.Instance;
            projectile = player.Arrow;
            projectiles = new List<Transform>();
        }

        public override void Unequip()
        {
            base.Unequip();
        }

        protected override void OnInputPerformed(InputAction.CallbackContext obj)
        {
            base.OnInputPerformed(obj);

            if (CanAttack() && !charging)
            {
                charging = true;
                animator.CrossFade("Charge_Start", 0.1f, 0);
                player.MovementStateMachine.ChangeState(player.MovementStateMachine.ChargeAttackingState);
                chargeStartTime = Time.time;

                projectiles.Add(Object.Instantiate(projectile, armory.CurrentWeaponBehavior.transform));
            }
        }

        protected override void OnInputCanceled(InputAction.CallbackContext obj)
        {
            base.OnInputCanceled(obj);

            if (charging)
            {
                charging = false;
                animator.SetTrigger("Charge");
                chargeEndTime = Time.time;

                Rigidbody rb = projectiles.Last().gameObject.AddComponent<Rigidbody>();
                rb.AddForce(player.transform.forward * 10, ForceMode.VelocityChange);

                projectiles.Last().GetComponent<Projectile>().OnHit += OnTriggerEnter;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsAttacking())
                return;

            //using a placeholder Health component to determine if collider hit is a valid target
            //use desired method in production (tag, interface etc.)
            var enemy = other.GetComponent<Enemy>();

            if (enemy == null) return;

            var health = enemy.GetComponent<EnemyStats>().Health;

            //Get a value between 0 and 1 based on how long the attack was charged
            var chargeDuration = chargeEndTime - chargeStartTime;
            var strength = Mathf.InverseLerp(0, data.maxHoldTime, chargeDuration);

            var hitData = new HitData()
            {
                Attacker = animator.gameObject,
                Target = enemy.gameObject,
                Weapon = armory.CurrentWeapon,
                HitTime = Time.time,
                HitStrength = strength

                //The HitData class has more fields as example of what data should be passed in
                //Feel free to add or remove fields based on requirements.
            };

            //Run all OnHitEffects to using the HitData for this attack
            var onHitBehaviors = data.onHitEffect.components.Select(e => e.GetBehavior()).ToList();
            onHitBehaviors.ForEach(b => b.OnHit(hitData));

            projectiles.Last().GetComponent<Projectile>().OnHit -= OnTriggerEnter;
        }

        //Maybe a bit too simple but we see what state the animator is currently in
        private bool CanAttack()
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);

            var match = false;

            foreach (var stateName in data.validStates)
            {
                if (state.IsName(stateName))
                {
                    match = true;
                    break;
                }
            }

            return match;
        }

        private bool IsAttacking()
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName("Charge_Attack");
        }
    }
}
