using KinematicCharacterController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Gameplay
{
    public class Player : Actor
    {
        public PlayerCamera defaultCamera;
        public Transform camFollowPoint;
        [Header("Input")]
        public InputActionReference lookInput;
        public InputActionReference zoomInput;
        public InputActionReference moveInput;
        public InputActionReference walkInput;
        public InputActionReference sprintInput;
        public InputActionReference jumpInput;
        public InputActionReference dashInput;

        private Movement movement;

        private void Start()
        {
            movement = GetComponent<Movement>();
            Cursor.lockState = CursorLockMode.Locked;

            defaultCamera.SetFollowTransform(camFollowPoint);
            defaultCamera.IgnoredColliders.Clear();
            defaultCamera.IgnoredColliders.AddRange(GetComponentsInChildren<Collider>());
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
                defaultCamera.PlanarDirection = motor.AttachedRigidbody
                    .GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * defaultCamera.PlanarDirection;
                defaultCamera.PlanarDirection = Vector3.ProjectOnPlane
                    (defaultCamera.PlanarDirection, motor.CharacterUp).normalized;
            }

            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            var lookInputVector = lookInput.action.ReadValue<Vector2>();
            
            if (Cursor.lockState != CursorLockMode.Locked)
                lookInputVector = Vector3.zero;

            var scrollInput = zoomInput.action.ReadValue<float>();
            defaultCamera.SetInputs(Time.deltaTime, scrollInput, lookInputVector);
        }

        private void HandleCharacterInput()
        {
            var input = new MovementInputs()
            {
                MoveInput = moveInput.action.ReadValue<Vector2>(),
                CameraRotation = defaultCamera.transform.rotation,
                Jump = jumpInput.action.WasPressedThisFrame(),
                Walk = walkInput.action.IsPressed(),
                Sprint = sprintInput.action.IsPressed(),
                Dash = dashInput.action.WasPressedThisFrame()
            };

            movement.SetInputs(input);
        }
    }
}
