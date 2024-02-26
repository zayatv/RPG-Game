using System;
using UnityEngine;

namespace CombatSystem
{
    public class WeaponBehavior : MonoBehaviour
    {
        public Action<Collider> OnHit;
        public GameObject blood;
        public GameObject bloodPoint;

        private void OnTriggerEnter(Collider other)
        {
            //OnHit?.Invoke(other);
            if(other.tag=="Enemy")
            {
              GameObject obj=  Instantiate(blood, bloodPoint.transform.position, Quaternion.identity);
              Destroy(obj, 1.5f);
            }
        }
    }
}