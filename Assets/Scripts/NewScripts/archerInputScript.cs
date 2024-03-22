using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using CombatSystem;
public class archerInputScript : MonoBehaviour
{
    public bool equipBow = false;
    public GameObject actualBow, aimCamera;
    private Animator anim;
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public Transform firePoint; // Point from where the arrow will be fired
    public Camera mainCamera;// Reference to the main camera
    public float fireCooldown = 2f; // Cooldown between consecutive shots
    private float lastFireTime; // Time when the last shot was fired
    public float arrowSpeed = 10f; // Speed of the arrow
    public PlayerCapsuleColliderUtility ColliderUtility;

    public float mouseSensitivity = 100f; // Mouse sensitivity for camera rotation
   
    public GameObject playerCam;

    public Transform bow;
    public float rotationSpeed = 5f;
    public float verticalRotationLimit = 80f; // Limit the vertical rotation to avoid flipping

    public float verticalRotation = 0f;
    
    public bool allowFire = false;
    private Armory armory;

    public float bowVerticalMin = -300f; // Minimum vertical rotation
    public float bowVerticalMax = -70f;  // Maximum vertical rotation
   

    private void Start()
    {
      
        anim = GetComponentInChildren<Animator>();
        armory = GetComponent<Armory>();
        actualBow.SetActive(false);
        mainCamera = Camera.main;
    }

    public void setFireFalse()
    {
     
        anim.SetBool("fireArrow", false);
    }

    public void setFiringFalse()
    {
      
        fireCooldown = 0.7f;
    }
    private void Update()
    {
        if (allowFire == false ) {
            lastFireTime += Time.deltaTime;
            if(lastFireTime>fireCooldown)
            {
                   allowFire = true;
                   lastFireTime = 0;
            }


        }
        
        if (anim.GetBool("aimArrow")==true)
        {
           
            RotatePlayer();
           
        }
     
        if (Input.GetMouseButton(0)&& allowFire==true && equipBow==true)// Assuming left mouse button is used to fire
        {
            anim.SetBool("fireArrow", true);
            Fire();
            
            Invoke("setFireFalse", 0.5f);
            Invoke("setFiringFalse", 0.5f);
            setTrue();
           
        }
        if (equipBow == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
               
                anim.SetBool("Attacking", false);
                activatebow();
                armory.rightHand.gameObject.SetActive(false);
                
                equipBow = true;

            }
        }

        if (equipBow == true)
        {
            anim.SetBool("Attacking", false);
            if (Input.GetMouseButton(1))
            {
                
                mainCamera.enabled = false;
                aimCamera.GetComponent<Animator>().Play("aimIn");
                aimCamera.SetActive(true);
               
                transform.GetComponentInParent<Player>().enabled = false;
                transform.GetComponent<PlayerMovement>().enabled = true;
                

                anim.SetBool("aimArrow", true);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                aimCamera.GetComponent<Animator>().Play("aimOut");
                Invoke("Enable3rdPersonCam", 1.0f);
                transform.GetComponentInParent<Player>().enabled = true;
                transform.GetComponent<PlayerMovement>().enabled = false;
                
                anim.SetBool("aimArrow", false);
                anim.SetBool("fireArrow", false);
             
               
              transform.localEulerAngles = new Vector3(0, 0,0);
                bow.gameObject.SetActive(false);
                equipBow = false;
            }



        }
        
    }
    void Enable3rdPersonCam()
    {
        mainCamera.enabled = true;
        aimCamera.SetActive(false);
    }
    void Fire()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Raycast from the center of the screen into the scene
        Ray ray = aimCamera.GetComponent<Camera>().ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the direction from the arrow spawn point to the hit point
            Vector3 direction = hit.point - firePoint.position;

            // Instantiate the arrow and set its direction and speed
            GameObject arrow = Instantiate(arrowPrefab, firePoint.transform.position, firePoint.transform.rotation);
      
            arrow.GetComponent<Rigidbody>().velocity = direction.normalized * arrowSpeed;
            Destroy(arrow, 4);
        }

    }
        void setTrue()
    {
        allowFire = false;
    
    }
    private void activatebow()
    {

        actualBow.SetActive(true);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate horizontally
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);

        // Rotate vertically
        verticalRotation -= mouseY * rotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // Apply vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(verticalRotation, transform.localEulerAngles.y, 0f);

        // Calculate the clamped vertical rotation for the bow
        float clampedVerticalRotation = Mathf.Clamp(verticalRotation, bowVerticalMin, bowVerticalMax);
        bow.localEulerAngles = new Vector3(bow.localEulerAngles.x, bow.localEulerAngles.y, -clampedVerticalRotation);






        //Rotate the bow vertically

    }

 void otherRotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * 2;
        float mouseY = Input.GetAxis("Mouse Y") * 5;

        // Rotate the camera horizontally based on mouse movement
        aimCamera.GetComponent<Camera>().transform.Rotate(Vector3.up, mouseX);
        transform.GetComponentInParent<Transform>().Rotate(Vector3.up, mouseX);
        // Calculate vertical rotation
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80, 80);

        // Apply vertical rotation
        aimCamera.GetComponent<Camera>().transform.localRotation = Quaternion.Euler(verticalRotation, aimCamera.GetComponent<Camera>().transform.localEulerAngles.y, 0f);
    }



}
