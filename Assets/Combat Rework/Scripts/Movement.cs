using UnityEngine;

namespace CombatSystem
{
    public class Movement : MonoBehaviour
    {
        public float moveSpeed = 1;
        public float turnSpeed = 25;
        public float sprintMulti = 2f;
        public float groundCheckDistance = 0.2f;
        public LayerMask groundLayers;

        private Animator animator;
        private Rigidbody rb;

        public bool InCombat { get; set; }
        public bool IsGrounded { get; set; }

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        private void OnAnimatorMove()
        {
            //Movement is primarily from root motion built into animations
            //This function is used to override the rootmotion to be able to tweak speed
            if (IsGrounded && Time.deltaTime > 0)
            {
                var velocity = (animator.deltaPosition * moveSpeed) / Time.deltaTime;

                velocity.y = rb.velocity.y;
                rb.velocity = velocity;
            }
        }

        public void Move(Vector3 inputDir, bool sprint)
        {
            if (sprint)
                inputDir *= sprintMulti;

            IsGrounded = GroundCheck();
            animator.SetBool("OnGround", IsGrounded);
            //The animator has been setup with Jump/Falling states but no animations are currently put into the overrides.

            if (InCombat)
            {
                animator.SetFloat("Vertical", inputDir.z, 0.1f, Time.deltaTime);
                animator.SetFloat("Horizontal", inputDir.x, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat("Vertical", inputDir.magnitude, 0.1f, Time.deltaTime);
                animator.SetFloat("Horizontal", inputDir.x, 0.1f, Time.deltaTime);

                if (inputDir != Vector3.zero)
                {
                    var targetRot = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(inputDir), turnSpeed * Time.deltaTime);
                    transform.rotation = targetRot;
                }
            }
        }

        private bool GroundCheck()
        {
            var startPos = transform.position + Vector3.up * 0.1f;
            var ray = new Ray(startPos, Vector3.down);
            return Physics.Raycast(ray, groundCheckDistance, groundLayers);
        }

        //Animation pack being used has these events
        public void FootR()
        {

        }

        public void FootL()
        {

        }

        public void Hit()
        {

        }
    }
}