using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ShipMovement movement_controller;

    private Vector2 axis;
    public bool jump;

    private float jump_timer = 0.0f;
    public float JumpCooldown = 0.5f;

  
    public void Update()
    {
        var v_axis = Input.GetAxis("Vertical");
        var h_axis = Input.GetAxis("Horizontal");
        axis = new Vector2(h_axis, v_axis);
        if (jump_timer <= 0.0f)
        {
            jump = Input.GetButtonDown("Jump");
            if (jump)
            {
                movement_controller.DodgeTeleport(axis);
                jump = false;
                jump_timer = JumpCooldown;
            }
        }
    }

    public void FixedUpdate()
    {
        jump_timer -= ScaledTime.fixedDeltaTime;

        movement_controller.Move(axis);
        
    }
}
