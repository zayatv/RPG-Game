using UnityEngine;

namespace RPG.Combat.Utilities
{
    public abstract class ActionListComponent<T> : ScriptableObject where T : IActionListBehavior
    {
        public virtual string CustomName { get; }
        public abstract T GetBehavior();
    }

}
