using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

[CreateAssetMenu(menuName = "Movement Behaviors/Player")]
public class PlayerMovement : MovementBehavior
{
    public Movement movement_controller;

    public Vector2 Direction;
    public bool jump;

    private float jump_timer = 0.0f;
    public float JumpCooldown = 0.5f;

    public Boundary bounds;

    public override MovementPacket FixedApply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2), GameObject source = default(GameObject))
    {
        if (init)
        {
            prior = InitPacket(t, forward, position, source);
        }

        jump_timer -= ScaledTime.fixedDeltaTime;

        prior.direction += Direction;
        prior.data.boundary = bounds.Rect;
        prior.type = MovementType.Clamped;

        return prior;
    }

    public override MovementPacket Apply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2), GameObject source = default(GameObject))
    {
        if (init)
        {
            prior = InitPacket(t, forward, position, source);
            prior.type = MovementType.ClampedTeleport;
        }

        var v_axis = Input.GetAxis("Vertical");
        var h_axis = Input.GetAxis("Horizontal");
        Direction = new Vector2(h_axis, v_axis);
        if (jump_timer <= 0.0f)
        {
            jump = Input.GetButtonDown("Jump");
            if (jump)
            {
                jump = false;
                jump_timer = JumpCooldown;
                prior.direction += Direction.normalized;
                prior.data.boundary = bounds.Rect;
            }
        }

        return prior;
    }

}
