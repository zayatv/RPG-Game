using CombatSystem.Utilities;
using UnityEngine.InputSystem;

namespace CombatSystem
{
    public abstract class MovesetComponent : ActionListComponent<IMovesetBehavior>
    {
        public InputActionReference input;
    }
}