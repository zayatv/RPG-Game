using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileEffect : MonoBehaviour
{
    public GameObject blood;
    public Transform bloodPoint;
    private void OnTriggerEnter(Collider other)
    {
        //OnHit?.Invoke(other);
        if (other.tag == "Enemy")
        {
            GameObject obj = Instantiate(blood, bloodPoint.transform.position, Quaternion.identity);
            Destroy(obj, 1.5f);
        }
    }


}
