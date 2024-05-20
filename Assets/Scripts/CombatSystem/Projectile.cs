using System;
using UnityEngine;

namespace CombatSystem
{
    public class Projectile : MonoBehaviour
    {
        public Action<Collider> OnHit;

        private void OnTriggerEnter(Collider other)
        {
            OnHit?.Invoke(other);
        }
    }
}
