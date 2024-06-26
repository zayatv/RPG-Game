using KinematicCharacterController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Gameplay
{
    public class Player : Actor
    {
        [Header("Input")]
        public InputActionReference lookInput;
        public InputActionReference zoomInput;
        public InputActionReference moveInput;
        public InputActionReference walkInput;
        public InputActionReference sprintInput;
        public InputActionReference jumpInput;
        public InputActionReference dashInput;

        private Movement movement;
        private PlayerTargeting targeting;

        private PlayerCamera ActiveCam => targeting.ActiveCamera;

        private void Start()
        {
            movement = GetComponent<Movement>();
            targeting = GetComponent<PlayerTargeting>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;

            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            if (motor.AttachedRigidbody != null)
            {
                ActiveCam.PlanarDirection = motor.AttachedRigidbody
                    .GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * ActiveCam.PlanarDirection;
                ActiveCam.PlanarDirection = Vector3.ProjectOnPlane
                    (ActiveCam.PlanarDirection, motor.CharacterUp).normalized;
            }

            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            var lookInputVector = lookInput.action.ReadValue<Vector2>();
            
            if (Cursor.lockState != CursorLockMode.Locked)
                lookInputVector = Vector3.zero;

            var scrollInput = zoomInput.action.ReadValue<float>();
            ActiveCam.SetInputs(Time.deltaTime, scrollInput, lookInputVector);
        }

        private void HandleCharacterInput()
        {
            var input = new MovementInputs()
            {
                MoveInput = moveInput.action.ReadValue<Vector2>(),
                CameraRotation = ActiveCam.transform.rotation,
                Jump = jumpInput.action.WasPressedThisFrame(),
                Walk = walkInput.action.IsPressed(),
                Sprint = sprintInput.action.IsPressed(),
                Dash = dashInput.action.WasPressedThisFrame()
            };

            movement.SetInputs(input);
        }
    }
}
