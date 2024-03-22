using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
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
            Debug.Log("Input Performed");
            //Player.Instance.MovementStateMachine.ChangeState(Player.Instance.MovementStateMachine.chargeAttackingState);
          
        }

        protected virtual void OnInputCanceled(InputAction.CallbackContext obj)
        {
            //Player.Instance.MovementStateMachine.ChangeState(Player.Instance.MovementStateMachine.IdlingState);
            Debug.Log("Input Canceled");
        }


        protected virtual void ChargeOnInputPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("Input Performed");


          

            Player.Instance.MovementStateMachine.ChangeState(Player.Instance.MovementStateMachine.chargeAttackingState);

        }

        protected virtual void ChargeInputCanceled(InputAction.CallbackContext obj)
        {
           
         
            
            Player.Instance.Armory.leftHand.gameObject.SetActive(false);
          

            Player.Instance.CameraUtility.aimCamera.Priority = 0;



        }
    }
}