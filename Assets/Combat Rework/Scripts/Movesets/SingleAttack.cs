using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem.Movesets
{
    public class SingleAttack : MovesetComponent
    {
        public AnimationClip animation;
        [Tooltip("The state that will be run on the Animator to perform this attack.")]
        public string targetState = "Attack";
        [Tooltip("The Animator must be in one of these states to allow attacking.")]
        public List<string> validStates = new List<string> { "Movement_Exploration", "Movement_Combat" };
        public OnHitEffect onHitEffect;

        public override IMovesetBehavior GetBehavior()
        {
            return new SingleAttackBehavior(this);
        }
    }

    public class SingleAttackBehavior : MovesetBehavior<SingleAttack>
    {
        private Animator animator;
        private AnimatorOverrideController overrideController;
        private Armory armory;

        public SingleAttackBehavior(SingleAttack data) : base(data)
        {
        }

        public override void Equip(GameObject user)
        {
            base.Equip(user);

            animator = user.GetComponent<Animator>();

            //Change the attack animation on the controller
            //The animator for the player is an override controller by default which allows this to work
            //(see Combat Rework/Animations folder)
            overrideController = (AnimatorOverrideController)animator.runtimeAnimatorController;
            overrideController[data.targetState] = data.animation;

            armory = user.GetComponent<Armory>();
            armory.CurrentWeaponBehavior.OnHit += OnTriggerEnter;
        }

        public override void Unequip()
        {
            base.Unequip();

            armory.CurrentWeaponBehavior.OnHit -= OnTriggerEnter;
        }

        protected override void OnInputPerformed(InputAction.CallbackContext obj)
        {
            base.OnInputPerformed(obj);

            //Directly play the desired animation without bothering with transitions
            if (CanAttack())
                animator.CrossFade(data.targetState, 0.1f, 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsAttacking())
                return;

            //using a placeholder Health component to determine if collider hit is a valid target
            //use desired method in production (tag, interface etc.)
            var health = other.GetComponent<Health>();

            if (health != null)
            {
                var hitData = new HitData()
                {
                    Attacker = animator.gameObject,
                    Target = health.gameObject,
                    Weapon = armory.CurrentWeapon,
                    HitTime = Time.time
                    //The HitData class has more fields as example of what data should be passed in
                    //Feel free to add or remove fields based on requirements.
                };

                //Run all OnHitEffects to using the HitData for this attack
                var onHitBehaviors = data.onHitEffect.components.Select(e => e.GetBehavior()).ToList();
                onHitBehaviors.ForEach(b => b.OnHit(hitData));
            }
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
            return state.IsName(data.targetState);
        }
    }
}
