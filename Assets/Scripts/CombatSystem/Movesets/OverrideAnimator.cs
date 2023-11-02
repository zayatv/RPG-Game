using Sirenix.OdinInspector;
using UnityEngine;

namespace CombatSystem.Movesets
{
    [TypeInfoBox("Input field unused for this component.")]
    public class OverrideAnimator : MovesetComponent
    {
        public AnimatorOverrideController animatorOverride;
        [Tooltip("Return to previous animator when weapon is unequiped.")]
        public bool defaultWhenUnequipped = true;

        public override IMovesetBehavior GetBehavior()
        {
            return new OverrideAnimatorBehavior(this);
        }
    }

    public class OverrideAnimatorBehavior : MovesetBehavior<OverrideAnimator>
    {
        private Animator animator;
        private RuntimeAnimatorController previousController;

        public OverrideAnimatorBehavior(OverrideAnimator data) : base(data)
        {
        }

        public override void Equip(GameObject user)
        {
            base.Equip(user);

            animator = user.GetComponent<Animator>();
            previousController = animator.runtimeAnimatorController;

            animator.runtimeAnimatorController = data.animatorOverride;
        }

        public override void Unequip()
        {
            base.Unequip();

            if (data.defaultWhenUnequipped)
                animator.runtimeAnimatorController = previousController;
        }
    }
}
