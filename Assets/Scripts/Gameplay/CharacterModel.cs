using Animancer;
using UnityEngine;

namespace RPG.Gameplay
{
    public class CharacterModel : MonoBehaviour
    {
        public Transform rightHand;
        public Transform leftHand;

        private HybridAnimancerComponent animator;

        public HybridAnimancerComponent Animator
        {
            get => animator == null ? animator = GetComponent<HybridAnimancerComponent>() : animator;
        }
    }
}
