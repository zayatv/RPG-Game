using UnityEngine;

namespace RPG.Combat
{
    public class Arrow : MonoBehaviour
    {
        public float shootForce = 50f;
        public float duration = 5f;

        public OnHitEffect OnHit { get; set; }

        public void Shoot()
        {
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
            Destroy(gameObject, duration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<Enemy>() != null)
            {
                var rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
}