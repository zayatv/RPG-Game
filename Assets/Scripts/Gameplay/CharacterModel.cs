using Animancer;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RPG.Gameplay
{
    public class CharacterModel : MonoBehaviour
    {
        public Transform rightHand;
        public Transform leftHand;
        public MultiAimConstraint spineAim;
        public Transform aimTarget;

        private HybridAnimancerComponent animator;
        private Rig ikRig;

        public Vector3 AccumulatedRootMotion { get; set; }
        public Quaternion AccumulatedRootRotation { get; set; }

        public HybridAnimancerComponent Animator
        {
            get => animator == null ? animator = GetComponent<HybridAnimancerComponent>() : animator;
        }
        public Rig IKRig
        {
            get => ikRig == null ? ikRig = GetComponentInChildren<Rig>() : ikRig;
        }

        private void OnAnimatorMove()
        {
            if (animator.applyRootMotion)
            {
                AccumulatedRootMotion += animator.deltaPosition;
                AccumulatedRootRotation = animator.deltaRotation * AccumulatedRootRotation;
            }
        }
    }
}
