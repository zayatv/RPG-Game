using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollowArrow : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab of the object to spawn
     // Reference to the camera used for raycasting
    public GameObject firePoint; // Reference to the fire point GameObject

    private GameObject spawnedObject; // Reference to the spawned object
    public LayerMask layers;
    public archerInputScript bowcheck;

    private void Start()
    {
        
    }
    void Update()
    {
        if (bowcheck.equipBow == true)
        {

            RaycastHit hit;
            Ray ray = new Ray(firePoint.transform.position, -firePoint.transform.right);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layers))
            {

                // Spawn the object at the hit point if it's not already spawned
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(objectPrefab, hit.point, Quaternion.identity);
                }
                else
                {
                    // Update the position of the spawned object if it's already spawned
                    spawnedObject.transform.position = hit.point;
                }
            }
        }

        else
        {
            //Destroy the spawned object if Fire1 button is not pressed
            Destroy(spawnedObject);
        }

    }
}
