using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem.Movesets
{
    public class SingleAttack : MovesetComponent
    {
        public AnimationClip animation;
        public string targetState = "Attack";
        public List<string> validStates = new List<string> { "Movement_Exploration", "Movement_Combat" };

        public override IMovesetBehavior GetBehavior()
        {
            return new SingleAttackBehavior(this);
        }
    }

    public class SingleAttackBehavior : MovesetBehavior<SingleAttack>
    {
        private Animator animator;
        private AnimatorOverrideController overrideController;

        public SingleAttackBehavior(SingleAttack data) : base(data)
        {
        }

        public override void Equip(GameObject user)
        {
            base.Equip(user);

            animator = user.GetComponent<Animator>();
            overrideController = (AnimatorOverrideController)animator.runtimeAnimatorController;

            overrideController[data.targetState] = data.animation;
        }

        public override void Unequip()
        {
            base.Unequip();
        }

        protected override void OnInputPerformed(InputAction.CallbackContext obj)
        {
            base.OnInputPerformed(obj);

            if (CanAttack())
                animator.CrossFade(data.targetState, 0.1f, 0);
        }

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
