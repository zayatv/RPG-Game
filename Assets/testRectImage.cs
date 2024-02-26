using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRectImage : MonoBehaviour
{
    public RectTransform imageRectTransform;
    public Transform targetTransform;
    public Camera aimCam;
    void Update()
    {
        if (imageRectTransform != null && targetTransform != null)
        {
            // Convert the target's world position to screen space
            Vector3 screenPos = aimCam.WorldToScreenPoint(targetTransform.position);

            // Set the UI image's position to the screen position
            imageRectTransform.position = screenPos;
        }
    }
}
