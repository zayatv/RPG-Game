using Animancer;
using UnityEngine;

namespace RPG.Gameplay
{
    public class CharacterModel : MonoBehaviour
    {
        public Transform rightHand;
        public Transform leftHand;

        private HybridAnimancerComponent animator;

        public Vector3 AccumulatedRootMotion { get; set; }
        public Quaternion AccumulatedRootRotation { get; set; }

        public HybridAnimancerComponent Animator
        {
            get => animator == null ? animator = GetComponent<HybridAnimancerComponent>() : animator;
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
