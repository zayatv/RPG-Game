using System.Collections.Generic;
using UnityEngine;

namespace RPG.Gameplay
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Framing")]
        public Vector2 followPointFraming = new Vector2(0f, 0f);
        public float followingSharpness = 10000f;

        [Header("Distance")]
        public float defaultDistance = 6f;
        public float minDistance = 0f;
        public float maxDistance = 10f;
        public float distanceMovementSpeed = 5f;
        public float distanceMovementSharpness = 10f;

        [Header("Rotation")]
        public bool invertX = false;
        public bool invertY = false;
        [Range(-90f, 90f)]
        public float defaultVerticalAngle = 20f;
        [Range(-90f, 90f)]
        public float minVerticalAngle = -90f;
        [Range(-90f, 90f)]
        public float maxVerticalAngle = 90f;
        public float rotationSpeed = 1f;
        public float rotationSharpness = 10000f;

        [Header("Collision")]
        public float collisionCheckRadius = 0.2f;
        public LayerMask collisionLayers = -1;
        public float collisionSharpness = 10000f;
        
        public List<Collider> IgnoredColliders { get; set; } = new List<Collider>();
        public Transform FollowTransform { get; private set; }
        public Vector3 PlanarDirection { get; set; }
        public float TargetDistance { get; set; }
        public float TargetVerticalAngle { get; set; }


        private bool distanceIsObstructed;
        private float currentDistance;
        private int obstructionCount;
        private RaycastHit[] obstructions = new RaycastHit[MaxObstructions];
        private Vector3 currentFollowPosition;

        private const int MaxObstructions = 32;

        void OnValidate()
        {
            defaultDistance = Mathf.Clamp(defaultDistance, minDistance, maxDistance);
            defaultVerticalAngle = Mathf.Clamp(defaultVerticalAngle, minVerticalAngle, maxVerticalAngle);
        }

        void Awake()
        {
            currentDistance = defaultDistance;
            TargetDistance = currentDistance;

            TargetVerticalAngle = 0f;

            PlanarDirection = Vector3.forward;
        }

        // Set the transform that the camera will orbit around
        public void SetFollowTransform(Transform t)
        {
            FollowTransform = t;
            PlanarDirection = FollowTransform.forward;
            currentFollowPosition = FollowTransform.position;
        }

        public void SetInputs(float deltaTime, float zoomInput, Vector3 rotationInput)
        {
            if (FollowTransform)
            {
                if (invertX)
                {
                    rotationInput.x *= -1f;
                }
                if (invertY)
                {
                    rotationInput.y *= -1f;
                }

                // Process rotation input
                Quaternion rotationFromInput = Quaternion.Euler(FollowTransform.up * (rotationInput.x * rotationSpeed));
                PlanarDirection = rotationFromInput * PlanarDirection;
                PlanarDirection = Vector3.Cross(FollowTransform.up, Vector3.Cross(PlanarDirection, FollowTransform.up));
                Quaternion planarRot = Quaternion.LookRotation(PlanarDirection, FollowTransform.up);

                TargetVerticalAngle -= (rotationInput.y * rotationSpeed);
                TargetVerticalAngle = Mathf.Clamp(TargetVerticalAngle, minVerticalAngle, maxVerticalAngle);
                Quaternion verticalRot = Quaternion.Euler(TargetVerticalAngle, 0, 0);
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, planarRot * verticalRot, 1f - Mathf.Exp(-rotationSharpness * deltaTime));

                // Apply rotation
                transform.rotation = targetRotation;

                // Process distance input
                if (distanceIsObstructed && Mathf.Abs(zoomInput) > 0f)
                {
                    TargetDistance = currentDistance;
                }
                TargetDistance += zoomInput * distanceMovementSpeed;
                TargetDistance = Mathf.Clamp(TargetDistance, minDistance, maxDistance);

                // Find the smoothed follow position
                currentFollowPosition = Vector3.Lerp(currentFollowPosition, FollowTransform.position, 1f - Mathf.Exp(-followingSharpness * deltaTime));

                // Handle obstructions
                {
                    RaycastHit closestHit = new RaycastHit();
                    closestHit.distance = Mathf.Infinity;
                    obstructionCount = Physics.SphereCastNonAlloc(currentFollowPosition, collisionCheckRadius, -transform.forward, obstructions, TargetDistance, collisionLayers, QueryTriggerInteraction.Ignore);
                    for (int i = 0; i < obstructionCount; i++)
                    {
                        bool isIgnored = false;
                        for (int j = 0; j < IgnoredColliders.Count; j++)
                        {
                            if (IgnoredColliders[j] == obstructions[i].collider)
                            {
                                isIgnored = true;
                                break;
                            }
                        }
                        for (int j = 0; j < IgnoredColliders.Count; j++)
                        {
                            if (IgnoredColliders[j] == obstructions[i].collider)
                            {
                                isIgnored = true;
                                break;
                            }
                        }

                        if (!isIgnored && obstructions[i].distance < closestHit.distance && obstructions[i].distance > 0)
                        {
                            closestHit = obstructions[i];
                        }
                    }

                    // If obstructions detecter
                    if (closestHit.distance < Mathf.Infinity)
                    {
                        distanceIsObstructed = true;
                        currentDistance = Mathf.Lerp(currentDistance, closestHit.distance, 1 - Mathf.Exp(-collisionSharpness * deltaTime));
                    }
                    // If no obstruction
                    else
                    {
                        distanceIsObstructed = false;
                        currentDistance = Mathf.Lerp(currentDistance, TargetDistance, 1 - Mathf.Exp(-distanceMovementSharpness * deltaTime));
                    }
                }

                // Find the smoothed camera orbit position
                Vector3 targetPosition = currentFollowPosition - ((targetRotation * Vector3.forward) * currentDistance);

                // Handle framing
                targetPosition += transform.right * followPointFraming.x;
                targetPosition += transform.up * followPointFraming.y;

                // Apply position
                transform.position = targetPosition;
            }
        }
    }
}
