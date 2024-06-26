using Animancer;
using System;

namespace RPG.Combat
{
    [Serializable]
    public class AttackMotion
    {
        public ClipTransition animation;
        public bool rootMotion = true;
        public OnHitEffect onHitEffect;
    }
}