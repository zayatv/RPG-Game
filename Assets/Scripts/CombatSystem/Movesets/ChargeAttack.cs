﻿using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem.Movesets
{
    public class ChargeAttack : MovesetComponent
    {
        //We set the charge animations on the Animator Override for the weapon directly but they
        //absolutely can be changed here through code (see SingleAttack for reference)
        //Just showing all the possible ways Animator Override Controllers can be used.

        public float maxHoldTime = 2f;
        [Tooltip("The Animator must be in one of the states having these tags to allow attacking.")]
        public List<string> validStates = new List<string> { "Grounded" };
        public OnHitEffect onHitEffect;
        public override IMovesetBehavior GetBehavior()
        {
            return new ChargeAttackBehavior(this);
        }
    }

    public class ChargeAttackBehavior : MovesetBehavior<ChargeAttack>
    {
        private Animator animator;
        private Armory armory;
        private bool charging;
        private float chargeStartTime;
        private float chargeEndTime;
        private Player player;

        public ChargeAttackBehavior(ChargeAttack data) : base(data)
        {
            
        }

        public override void Equip(GameObject user)
        {
            base.Equip(user);

            animator = user.GetComponent<Animator>();
            armory = user.GetComponent<Armory>();
            player = Player.Instance;

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

            if (CanAttack() && !charging)
            {
                charging = true;
                animator.CrossFadeInFixedTime("Charge_Start", 0.1f, 0);
                player.MovementStateMachine.ChangeState(player.MovementStateMachine.ChargeAttackingState);
                chargeStartTime = Time.time;
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
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsAttacking())
                return;

            Debug.Log("Test");

            //using a placeholder Health component to determine if collider hit is a valid target
            //use desired method in production (tag, interface etc.)
            var enemy = other.GetComponent<Enemy>();
            var health = enemy.GetComponent<EnemyStats>().Health;

            if (health == null) return;

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
        }

        //Maybe a bit too simple but we see what state the animator is currently in
        private bool CanAttack()
        {
            var state = animator.GetCurrentAnimatorStateInfo(0);

            var match = false;

            foreach (var stateTag in data.validStates)
            {
                if (state.IsTag(stateTag))
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
