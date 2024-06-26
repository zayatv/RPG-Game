using Animancer;
using KinematicCharacterController;
using UnityEngine;

namespace RPG.Gameplay
{
    public class Movement : MonoBehaviour, ICharacterController
    {
        [Header("Movement")]
        public float walkSpeed = 2f;
        public float runSpeed = 4f;
        public float sprintSpeed = 6f;
        public float movementSharpness = 15f;
        public float rotationSharpness = 10f;
        public float verticalRotationSharpness = 10f;

        [Header("Air")]
        public float airMoveSpeed = 15f;
        public float airAcceleration = 15f;
        public float drag = 0.1f;
        public Vector3 gravity = new Vector3(0, -30f, 0);

        [Header("Jumping")]
        public bool allowJumpingWhenSliding = false;
        public float jumpUpSpeed = 10f;
        public float jumpForwardSpeed = 10f;
        public float jumpPreGroundingGraceTime = 0f;
        public float jumpPostGroundingGraceTime = 0f;

        [Header("Dashing")]
        public float dashDistance = 4f;
        public float dashTime = 1f;
        public float dashCooldown = 0.4f;
        public bool allowDashWhileAirborne = true;

        private Vector3 moveInput;
        private Vector3 lookInput;
        private float currentMoveSpeed;

        private bool jumpRequested = false;
        private bool jumpConsumed = false;
        private bool jumpedThisFrame = false;
        private float timeSinceJumpRequested = Mathf.Infinity;
        private float timeSinceLastAbleToJump = 0f;

        private float dashStartTime = 0f;
        private float lastDashTime = 0f;

        private Actor actor;
        private KinematicCharacterMotor motor;

        private HybridAnimancerComponent Animator => actor.CurrentAnimator;
        public bool isDashing { get; private set; }
        public MoveState MoveState { get; private set; }
        public Vector3 OverrideLookDir { get; set; } //Override current rotation and look in the given direction instead

        private void Start()
        {
            actor = GetComponent<Actor>();
            motor = actor.Motor;
            motor.CharacterController = this;
        }

        public void SetInputs(MovementInputs input)
        {
            var moveInputVector = Vector3.ClampMagnitude(new Vector3(input.MoveInput.x, 0f, input.MoveInput.y), 1f);
            
            // Calculate camera direction and rotation on the character plane
            var cameraPlanarDirection = Vector3.ProjectOnPlane(input.CameraRotation * Vector3.forward, motor.CharacterUp).normalized;
            
            if (cameraPlanarDirection.sqrMagnitude == 0f)
                cameraPlanarDirection = Vector3.ProjectOnPlane(input.CameraRotation * Vector3.up, motor.CharacterUp).normalized;
            
            var cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, motor.CharacterUp);

            //Set inputs
            moveInput = cameraPlanarRotation * moveInputVector;
            lookInput = moveInput;

            if (moveInput.magnitude <= 0f)
            {
                currentMoveSpeed = 0f;
                MoveState = MoveState.Idle;
            }
            else if (input.Sprint)
            {
                currentMoveSpeed = sprintSpeed;
                MoveState = MoveState.Sprinting;
            }
            else if (input.Walk)
            {
                currentMoveSpeed = walkSpeed;
                MoveState = MoveState.Walking;
            }
            else
            {
                currentMoveSpeed = runSpeed;
                MoveState = MoveState.Running;
            }

            if (input.Dash && Time.time >= lastDashTime + dashCooldown && !Animator.applyRootMotion)
            {
                //Only dash when grounded if air dashes aren't allowed
                if (motor.GroundingStatus.FoundAnyGround || allowDashWhileAirborne)
                {
                    isDashing = true;
                    dashStartTime = Time.time;
                    lastDashTime = Time.time;

                    Animator.SetBool("Dash", true);
                }
            }

            if (input.Jump && !isDashing && !Animator.applyRootMotion)
            {
                timeSinceJumpRequested = 0f;
                jumpRequested = true;
            }
        }

        #region Controller Functions
        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            //If override look direction with direction to current target if there is one.
            if (OverrideLookDir.sqrMagnitude > 0f)
                lookInput = OverrideLookDir;
            
            if (lookInput.sqrMagnitude > 0f && rotationSharpness > 0f)
            {
                // Smoothly interpolate from current to target look direction
                var smoothedLookInputDirection = Vector3.Slerp(motor.CharacterForward, lookInput,
                    1 - Mathf.Exp(-rotationSharpness * deltaTime)).normalized;
                currentRotation = Quaternion.LookRotation(smoothedLookInputDirection, motor.CharacterUp);
            }

            var currentUp = currentRotation * Vector3.up;
            var smoothedGravityDir = Vector3.Slerp(currentUp, -gravity.normalized, 1 - Mathf.Exp(-verticalRotationSharpness * deltaTime));
            currentRotation = Quaternion.FromToRotation(currentUp, smoothedGravityDir) * currentRotation;

            if (Animator.applyRootMotion)
                currentRotation = actor.CharacterModel.AccumulatedRootRotation * currentRotation;
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            if (Animator.applyRootMotion && deltaTime > 0)
            {
                currentVelocity = actor.CharacterModel.AccumulatedRootMotion / deltaTime;
                currentVelocity = motor.GetDirectionTangentToSurface(currentVelocity, 
                    motor.GroundingStatus.GroundNormal) * currentVelocity.magnitude;
            }
            else if (isDashing)
                currentVelocity = HandleDashing(currentVelocity, deltaTime);
            else if (motor.GroundingStatus.IsStableOnGround)
                currentVelocity = HandleGroundedMovement(currentVelocity, deltaTime);
            else
                currentVelocity = HandleAirborneMovement(currentVelocity, deltaTime);

            currentVelocity = HandleJumping(currentVelocity, deltaTime);

            var normalizedSpeed = 0f;

            if (currentMoveSpeed == walkSpeed)
                normalizedSpeed = 0.5f;
            else if (currentMoveSpeed == runSpeed)
                normalizedSpeed = 1f;
            else if (currentMoveSpeed == sprintSpeed)
                normalizedSpeed = 2f;

            Animator.SetFloat("Speed", normalizedSpeed, 0.1f, deltaTime);
            Animator.SetBool("Grounded", motor.GroundingStatus.IsStableOnGround);
            Animator.SetBool("Moving", moveInput.sqrMagnitude > 0);
        }

        public void AfterCharacterUpdate(float deltaTime)
        {
            // Handle jumping pre-ground grace period
            if (jumpRequested && timeSinceJumpRequested > jumpPreGroundingGraceTime)
            {
                jumpRequested = false;
            }

            if (allowJumpingWhenSliding ? motor.GroundingStatus.FoundAnyGround : motor.GroundingStatus.IsStableOnGround)
            {
                // If we're on a ground surface, reset jumping values
                if (!jumpedThisFrame)
                {
                    jumpConsumed = false;
                }
                timeSinceLastAbleToJump = 0f;
            }
            else
            {
                // Keep track of time since we were last able to jump (for grace period)
                timeSinceLastAbleToJump += deltaTime;
            }

            if (Animator.applyRootMotion)
            {
                actor.CharacterModel.AccumulatedRootMotion = Vector3.zero;
                actor.CharacterModel.AccumulatedRootRotation = Quaternion.identity;
            }
        }
        #endregion

        #region Movement Functions
        private Vector3 HandleDashing(Vector3 currentVelocity, float deltaTime)
        {
            if (Time.time <= dashStartTime + dashTime)
                currentVelocity = motor.CharacterForward * (dashDistance / dashTime);
            else
            {
                Animator.SetBool("Dash", false);
                isDashing = false;
            }

            return currentVelocity;
        }

        private Vector3 HandleGroundedMovement(Vector3 currentVelocity, float deltaTime)
        {
            float currentVelocityMagnitude = currentVelocity.magnitude;

            Vector3 effectiveGroundNormal = motor.GroundingStatus.GroundNormal;

            // Reorient velocity on slope
            currentVelocity = motor.GetDirectionTangentToSurface(currentVelocity, effectiveGroundNormal) * currentVelocityMagnitude;

            // Calculate target velocity
            Vector3 inputRight = Vector3.Cross(moveInput, motor.CharacterUp);
            Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * moveInput.magnitude;
            Vector3 targetMovementVelocity = reorientedInput * currentMoveSpeed;

            // Smooth movement Velocity
            currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity, 1f - Mathf.Exp(-movementSharpness * deltaTime));
            return currentVelocity;
        }

        private Vector3 HandleAirborneMovement(Vector3 currentVelocity, float deltaTime)
        {
            if (moveInput.sqrMagnitude > 0f)
            {
                Vector3 addedVelocity = moveInput * airAcceleration * deltaTime;

                Vector3 currentVelocityOnInputsPlane = Vector3.ProjectOnPlane(currentVelocity, motor.CharacterUp);

                // Limit air velocity from inputs
                if (currentVelocityOnInputsPlane.magnitude < airMoveSpeed)
                {
                    // clamp addedVel to make total vel not exceed max vel on inputs plane
                    Vector3 newTotal = Vector3.ClampMagnitude(currentVelocityOnInputsPlane + addedVelocity, airMoveSpeed);
                    addedVelocity = newTotal - currentVelocityOnInputsPlane;
                }
                else
                {
                    // Make sure added vel doesn't go in the direction of the already-exceeding velocity
                    if (Vector3.Dot(currentVelocityOnInputsPlane, addedVelocity) > 0f)
                    {
                        addedVelocity = Vector3.ProjectOnPlane(addedVelocity, currentVelocityOnInputsPlane.normalized);
                    }
                }

                // Prevent air-climbing sloped walls
                if (motor.GroundingStatus.FoundAnyGround)
                {
                    if (Vector3.Dot(currentVelocity + addedVelocity, addedVelocity) > 0f)
                    {
                        Vector3 perpenticularObstructionNormal = Vector3.Cross(Vector3.Cross(motor.CharacterUp, motor.GroundingStatus.GroundNormal), motor.CharacterUp).normalized;
                        addedVelocity = Vector3.ProjectOnPlane(addedVelocity, perpenticularObstructionNormal);
                    }
                }

                // Apply added velocity
                currentVelocity += addedVelocity;
            }

            // Gravity
            currentVelocity += gravity * deltaTime;

            // Drag
            currentVelocity *= (1f / (1f + (drag * deltaTime)));
            return currentVelocity;
        }

        private Vector3 HandleJumping(Vector3 currentVelocity, float deltaTime)
        {
            jumpedThisFrame = false;
            timeSinceJumpRequested += deltaTime;

            if (jumpRequested)
            {
                // See if we actually are allowed to jump
                if (!jumpConsumed && ((allowJumpingWhenSliding ? motor.GroundingStatus.FoundAnyGround : motor.GroundingStatus.IsStableOnGround) || timeSinceLastAbleToJump <= jumpPostGroundingGraceTime))
                {
                    // Calculate jump direction before ungrounding
                    Vector3 jumpDirection = motor.CharacterUp;
                    if (motor.GroundingStatus.FoundAnyGround && !motor.GroundingStatus.IsStableOnGround)
                    {
                        jumpDirection = motor.GroundingStatus.GroundNormal;
                    }

                    // Makes the character skip ground probing/snapping on its next update. 
                    // If this line weren't here, the character would remain snapped to the ground when trying to jump. Try commenting this line out and see.
                    motor.ForceUnground();

                    // Add to the return velocity and reset jump state
                    currentVelocity += (jumpDirection * jumpUpSpeed) - Vector3.Project(currentVelocity, motor.CharacterUp);
                    currentVelocity += (moveInput * jumpForwardSpeed);
                    jumpRequested = false;
                    jumpConsumed = true;
                    jumpedThisFrame = true;

                    //Update the animator
                    Animator.SetTrigger("Jump");
                    Animator.SetBool("Grounded", false);
                }
            }

            return currentVelocity;
        }
        #endregion

        #region Unused Controller Functions
        public void BeforeCharacterUpdate(float deltaTime)
        {
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void PostGroundingUpdate(float deltaTime)
        {
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
        }
        #endregion
    }
}
