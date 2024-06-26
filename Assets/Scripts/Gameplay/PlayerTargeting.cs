using System.Linq;
using UnityEngine;

namespace RPG.Gameplay
{
    public class PlayerTargeting : MonoBehaviour
    {
        [SerializeField] private LayerMask detectionLayers;
        [SerializeField] private PlayerCamera defaultCam;
        [SerializeField] private PlayerCamera aimCam;
        [SerializeField] private Transform camFollowPoint;
        [SerializeField] private RectTransform aimCrosshair;

        private Transform mainCam;
        private Movement movement;
        private Actor actor;
        
        private bool aiming;

        public PlayerCamera ActiveCamera { get; private set; }
        public Transform AimTarget => actor.CharacterModel.aimTarget;

        private void Start()
        {
            mainCam = Camera.main.transform;
            movement = GetComponent<Movement>();
            actor = GetComponent<Actor>();
            
            ActivateCamera(defaultCam);
        }

        private void Update()
        {
            actor.CharacterModel.IKRig.weight = aiming ? 1f : 0f;

            if (!aiming)
                return;

            //Rotate character to face where the camera is looking
            var cameraPlanarDirection = Vector3.ProjectOnPlane(ActiveCamera.transform.rotation * Vector3.forward,
                transform.up).normalized;

            if (cameraPlanarDirection.sqrMagnitude == 0f)
                cameraPlanarDirection = Vector3.ProjectOnPlane(ActiveCamera.transform.rotation * Vector3.up,
                    transform.up).normalized;

            movement.OverrideLookDir = cameraPlanarDirection;

            //Update the target point for where you shoot
            var screenPoint = Camera.main.ScreenToWorldPoint(aimCrosshair.anchoredPosition);
            screenPoint += mainCam.forward * 10f;

            AimTarget.position = screenPoint;
            AimTarget.rotation = mainCam.rotation;
        }

        public void StartAiming()
        {
            ActivateCamera(aimCam);

            aimCrosshair.gameObject.SetActive(true);
            aiming = true;
        }

        public void StopAiming()
        {
            ActivateCamera(defaultCam);

            aimCrosshair.gameObject.SetActive(false);

            movement.OverrideLookDir = Vector3.zero;
            aiming = false;
        }

        /// <summary>
        /// Find the closest target in the direction the player is looking.
        /// </summary>
        /// <param name="radius">Search radius around the player.</param>
        /// <param name="maxAngle">How lenient the look direction should be. 
        /// Lower values would require you to look more directly at the target.</param>
        /// <returns>The most suitable target. null if none were found.</returns>
        public Transform FindTarget(float radius, float maxAngle = 20f)
        {
            var potentialTargets = Physics.OverlapSphere(transform.position, radius, detectionLayers)
                .Where(t => t.GetComponent<Enemy>() != null).ToArray();
            
            Transform closestTarget = null;
            var closestDistance = Mathf.Infinity;

            foreach (Collider targetCollider in potentialTargets)
            {
                var targetTransform = targetCollider.transform;
                var directionToTarget = (targetTransform.position - mainCam.position).normalized;
                var angle = Vector3.Angle(mainCam.forward, directionToTarget);

                if (angle < maxAngle)
                {
                    var distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestTarget = targetTransform;
                    }
                }
            }

            return closestTarget;
        }

        public void ActivateCamera(PlayerCamera camera)
        {
            camera.gameObject.SetActive(true);
            camera.SetFollowTransform(camFollowPoint);
            camera.IgnoredColliders.Clear();
            camera.IgnoredColliders.AddRange(GetComponentsInChildren<Collider>());

            if (ActiveCamera != null)
            {
                camera.PlanarDirection = ActiveCamera.PlanarDirection;
                camera.TargetVerticalAngle = ActiveCamera.TargetVerticalAngle;
                ActiveCamera.gameObject.SetActive(false);
                ActiveCamera = null;
            }

            ActiveCamera = camera;
        }
    }
}
