using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerAim : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the camera follows the player
    public float rotationSpeed = 5f; // Speed at which the camera rotates to align with player's aim
    public float minY = -30f; // Minimum Y position for the camera
    public float maxY = 30f; // Maximum Y position for the camera

    private void LateUpdate()
    {
        if (player == null)
            return;

        // Move the camera to follow the player smoothly
        transform.position = Vector3.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);

        // Get the player's aim direction (forward direction)
        Vector3 aimDirection = player.forward;

        // Align the camera's forward direction with the player's aim direction smoothly
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
        targetRotation.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Clamp the camera's Y position
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }
}
