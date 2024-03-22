using System;
using UnityEngine;

namespace CombatSystem
{
    public class WeaponBehavior : MonoBehaviour
    {
        public Action<Collider> OnHit;
        public GameObject blood;
        public GameObject bloodPoint;
        public bool isRangedWeapon = false;

        public Transform firePoint;
        public GameObject arrowPrefab;
        public float arrowSpeed;
        public Camera aimCamera;

        public float fireCooldown = 1;
        private bool allowFire = false;


        public Player player;
        public Animator anim;
        public bool isAttacking = false;
        public float lastMousePressTime = 0f;
        public float mousePressTimeout = 2f;


        private float lastFireTime;
        private void Start()
        {
            aimCamera = Camera.main;
            player = FindObjectOfType<Player>();
            anim = FindObjectOfType<Player>().transform.gameObject.GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            if (isRangedWeapon == true)
            {
                
                {
                    player.Armory.rightHand.gameObject.SetActive(false);
                    if (allowFire == false)
                    {
                        lastFireTime += Time.deltaTime;
                        if (lastFireTime > fireCooldown)
                        { 
                            lastFireTime = 0;
                            allowFire = true;
                           

                        }


                    }

                    Quaternion currentAimCameraRotation = Player.Instance.CameraUtility.aimCamera.transform.rotation;

                    // Rotate the aim camera to look at the virtual camera's position
                    Player.Instance.CameraUtility.aimCamera.transform.LookAt(Player.Instance.CameraUtility.VirtualCamera.transform.position);

                    // Calculate the look direction from the aim camera to the player
                    Vector3 lookDirection = Player.Instance.CameraUtility.aimCamera.transform.position - Player.Instance.transform.position;
                    lookDirection.y = 0f; // Keep the look direction horizontal
                    lookDirection *= -1f;

                    // Rotate the player to look in that direction
                    if (lookDirection != Vector3.zero)
                    {
                        Player.Instance.transform.rotation = Quaternion.LookRotation(lookDirection);
                    }

                    // Restore the aim camera's original rotation
                    Player.Instance.CameraUtility.aimCamera.transform.rotation = currentAimCameraRotation;


                }
            }


            else
            {
                if (Input.GetMouseButtonDown(0)) // Assuming left mouse button (0) is used for attacking
                {
                    isAttacking = true;
                    lastMousePressTime = Time.time;
                }

                // Update "Attacking" parameter in the animator
                if (anim.GetBool("Attacking") != isAttacking)
                {
                    anim.SetBool("Attacking", isAttacking);
                }

                // If mouse button hasn't been pressed for the last 2 seconds, set isAttacking to false
                if (Time.time - lastMousePressTime > mousePressTimeout && isAttacking == true)
                {
                    isAttacking = false;
                    player.MovementStateMachine.ChangeState(player.MovementStateMachine.IdlingState);
                    lastMousePressTime = 0;
                }
            }
        }

        public void Fire()
        {
            if (allowFire == true)
            {
                /// Create the arrow at the firePoint's position and with the firePoint's rotation
                GameObject arrow = Instantiate(arrowPrefab, firePoint.transform.position, firePoint.transform.rotation);

                // Calculate the direction the arrow should fly
                Vector3 flyDirection = aimCamera.transform.forward;

                // Set the arrow's velocity to fly straight in the calculated direction
                arrow.GetComponent<Rigidbody>().velocity = flyDirection * arrowSpeed;

                // Destroy the arrow after 4 seconds
                Destroy(arrow, 4);

                // Prevent firing again until the arrow is destroyed
                allowFire = false;
            }

        }
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