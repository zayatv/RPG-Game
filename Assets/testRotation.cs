using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotation : MonoBehaviour
{
    public GameObject mainCam;
    void Update()
    {
        transform.LookAt(mainCam.transform.position);
    }
}
