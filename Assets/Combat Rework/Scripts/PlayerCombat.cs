using UnityEngine;
using UnityEngine.InputSystem;

namespace CombatSystem
{
    public class PlayerCombat : MonoBehaviour
    {
        public InputActionReference moveAction;
        public InputActionReference sprintAction;

        private Transform mainCam;
        private Movement movement;

        private void Start()
        {
            mainCam = Camera.main.transform;
            movement = GetComponent<Movement>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            var input = moveAction.action.ReadValue<Vector2>();
            var sprint = sprintAction.action.IsPressed();

            var move = (input.y * mainCam.forward) + (input.x * mainCam.right);
            move.y = 0f;
            move.Normalize();

            movement.Move(move, sprint);
        }
    }
}