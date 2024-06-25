using Animancer;
using System;

namespace RPG.Combat
{
    [Serializable]
    public class MeleeAttack
    {
        public ClipTransition animation;
        public bool rootMotion = true;
        public OnHitEffect onHitEffect;
    }
}