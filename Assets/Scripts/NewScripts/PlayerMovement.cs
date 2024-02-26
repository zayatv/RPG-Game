using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Player movement speed
    public float rotationSpeed = 300f; // Player rotation speed
   

    private Rigidbody rb;
    private bool isGrounded;
    private Animator anim;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * horizontalInput + transform.forward * verticalInput).normalized;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

     if(moveDirection.magnitude>0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving",false);
        }
       
    }
}