using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem.Movesets
{
    public class ComboAttack : MovesetComponent
    {
        public Attack[] combo;
        [Tooltip("The Animator must be in one of these states to allow attacking.")]
        public List<string> validStates = new List<string> { "Movement_Exploration", "Movement_Combat" };

        public override IMovesetBehavior GetBehavior()
        {
            return new ComboAttackBehavior(this);
        }

        [System.Serializable]
        public class Attack
        {
            public AnimationClip animation;
            public bool overrideDuration = false;
            [ShowIf(nameof(overrideDuration))] 
            public float duration = 2f;
            [Tooltip("Only register input for next attack after this amount of time.")]
            public float ignoreInputFor = 0f;
            public OnHitEffect onHitEffect;
        }
    }

    public class ComboAttackBehavior : MovesetBehavior<ComboAttack>
    {
        public ComboAttackBehavior(ComboAttack data) : base(data)
        {
        }

        private Animator animator;
        private AnimatorOverrideController overrideController;
        private Armory armory;

        private int nextAttackIndex;
        private float lastAttackTime;
        private bool attackQueued;
        private ComboAttack.Attack currentAttack;

        public override void Equip(GameObject user)
        {
            base.Equip(user);

            animator = user.GetComponent<Animator>();
            overrideController = (AnimatorOverrideController)animator.runtimeAnimatorController;

            armory = user.GetComponent<Armory>();
            armory.CurrentWeaponBehavior.OnHit += OnTriggerEnter;
        }

        public override void Unequip()
        {
            base.Unequip();

            armory.CurrentWeaponBehavior.OnHit -= OnTriggerEnter;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
        }
        

        protected override void OnInputPerformed(InputAction.CallbackContext obj)
        {
            base.OnInputPerformed(obj);

            if (nextAttackIndex > 0)
            {
                if (attackQueued)
                    return;

                var timePassed = Time.time - lastAttackTime;
                var length = currentAttack.overrideDuration ? currentAttack.duration : currentAttack.animation.length;

                if (timePassed > length)
                {
                    nextAttackIndex = 0;
                    return;
                }
                
                //Start the next attack after the current one finishes
                if (timePassed > currentAttack.ignoreInputFor)
                {
                    var timeLeft = length - timePassed;
                    armory.StartCoroutine(Attack(timeLeft));
                }
            }
            else
            {
                if (CanAttack())
                    armory.StartCoroutine(Attack(0f));
            }
            
            
        }

        //Coroutines can still be run on any MonoBehavior so we're free to use them if convenient
        private IEnumerator Attack(float queueTime)
        {
            attackQueued = true;

            yield return new WaitForSeconds(queueTime);

            attackQueued = false;

            var current = data.combo[nextAttackIndex];

            //We have two Attack states setup in the animator to cycle between for situations like this
            //This is just so we can transition from one attack to another without instantly snapping/changing out the animation
            var state = "Attack";

            if (nextAttackIndex % 2 != 0)
                state = "Attack_1";

            if (nextAttackIndex == 0)
                state = "Attack";

            overrideController[state] = current.animation;
            animator.CrossFade(state, 0.1f);

            lastAttackTime = Time.time;
            currentAttack = current;
            nextAttackIndex++;

            if (nextAttackIndex >= data.combo.Length)
                nextAttackIndex = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Create OnHitEffects similar to how it's done in SingleAttack
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
    }
}
