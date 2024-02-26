    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatSystem;
public class TestPlayerInput : MonoBehaviour
{
    public Player player;
    private Animator anim;
    public bool isAttacking = false;
    public float lastMousePressTime = 0f;
    public float mousePressTimeout = 2f;
    private Armory armory;
    public RectTransform myPanel;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        armory = GetComponentInChildren<Armory>();
       
    }
    void Update()
    {
        if (armory.equippedBow == false)
        {
            if (Input.GetMouseButtonDown(0)) // Assuming left mouse button (0) is used for attacking
            {
                isAttacking = true;
                lastMousePressTime = Time.time;
            }

            // Update "Attacking" parameter in the animator
            anim.SetBool("Attacking", isAttacking);

            // If mouse button hasn't been pressed for the last 2 seconds, set isAttacking to false
            if (Time.time - lastMousePressTime > mousePressTimeout && isAttacking == true)
            {
                isAttacking = false;
                player.MovementStateMachine.ChangeState(player.MovementStateMachine.IdlingState);
                lastMousePressTime = 0;
            }
        }
    }
}
