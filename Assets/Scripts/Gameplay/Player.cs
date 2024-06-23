using KinematicCharacterController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Gameplay
{
    public class Player : MonoBehaviour
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
        private KinematicCharacterMotor motor;
        private CharacterModel currentCharacter;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            movement = GetComponent<Movement>();
            motor = GetComponent<KinematicCharacterMotor>();

            defaultCamera.SetFollowTransform(camFollowPoint);
            defaultCamera.IgnoredColliders.Clear();
            defaultCamera.IgnoredColliders.AddRange(GetComponentsInChildren<Collider>());

            var startingCharacter = GetComponentInChildren<CharacterModel>();
            if (startingCharacter != null)
                UpdateCharacter(startingCharacter, false);
        }

        public void UpdateCharacter(CharacterModel character, bool isPrefab = true)
        {
            if (currentCharacter != null)
                Destroy(currentCharacter.gameObject);

            currentCharacter = isPrefab ? Instantiate(character) : character;
            currentCharacter.transform.SetParent(transform);
            currentCharacter.transform.localPosition = Vector3.zero;
            currentCharacter.transform.localRotation = Quaternion.identity;

            movement.Animator = currentCharacter.Animator;
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
