using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem
{
    public abstract class MovesetBehavior<T> : IMovesetBehavior where T : MovesetComponent
    {
        protected T data;

        public MovesetBehavior(T data)
        {
            this.data = data;
        }

        public virtual void Equip(GameObject user)
        {
            if (data.input != null)
            {
                data.input.action.performed += OnInputPerformed;
                data.input.action.canceled += OnInputCanceled;
            }
        }

        public virtual void Tick(float deltaTime)
        {

        }

        public virtual void Unequip()
        {
            if (data.input != null)
            {
                data.input.action.performed -= OnInputPerformed;
                data.input.action.canceled -= OnInputCanceled;
            }
        }

        protected virtual void OnInputPerformed(InputAction.CallbackContext obj)
        {

        }

        protected virtual void OnInputCanceled(InputAction.CallbackContext obj)
        {
        }
    }
}