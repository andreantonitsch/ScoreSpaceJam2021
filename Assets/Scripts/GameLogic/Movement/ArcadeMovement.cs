using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArcadeMovement : Movement
{
    public Rigidbody2D body;
    public Transform t;
    public float BaseSpeed;
    public float BaseDodgeDistance;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }


    public override void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
            body.MovePosition(body.position + direction * BaseSpeed * ScaledTime.fixedDeltaTime);
    }

    // MUST be called on Update
    public override void DodgeTeleport(Vector2 direction)
    {
        if(direction != Vector2.zero)
            t.position = body.position + direction * BaseDodgeDistance;
    }

    public override void Clamp(Vector4 bounds)
    {
        var p = body.position;
        var clamped_pos = new Vector2(Mathf.Min(bounds.x, Mathf.Max(p.x, bounds.z)),
                                      Mathf.Min(bounds.y, Mathf.Max(p.y, bounds.w)));
        body.MovePosition(clamped_pos);
    }

    public override void ClampMovement(Vector2 direction, Vector4 bounds)
    {
        var p = body.position + direction * BaseSpeed * ScaledTime.fixedDeltaTime;
        var clamped_pos = new Vector2(Mathf.Min(bounds.x, Mathf.Max(p.x, bounds.z)),
                                      Mathf.Min(bounds.y, Mathf.Max(p.y, bounds.w)));

        body.MovePosition(clamped_pos);
    }

    public override void ClampTeleport(Vector2 direction, Vector4 bounds)
    {
        if (direction == Vector2.zero) return;

        PlayerController.Player.GetComponent<TeleportAnimation>().Spawn(body.position);

        var p = body.position + direction * BaseDodgeDistance;
        var clamped_pos = new Vector2(Mathf.Min(bounds.x, Mathf.Max(p.x, bounds.z)),
                                      Mathf.Min(bounds.y, Mathf.Max(p.y, bounds.w)));
        t.position = clamped_pos;
    }


}
