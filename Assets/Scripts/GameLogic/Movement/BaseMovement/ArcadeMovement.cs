using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArcadeMovement : Movement
{
    private Rigidbody2D body;
    public Transform t;
    public float BaseSpeed;
    public float BaseDodgeDistance;

    public Rigidbody2D Body { get {
            if (body == null)
                body = GetComponent<Rigidbody2D>();
            return body;
        }
        set => body = value; }

    public delegate void TeleportTriggeredEventHandler();
    public event TeleportTriggeredEventHandler TeleportTriggeredEvent;
    public void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }


    public override void Move(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            Body.MovePosition(Body.position + direction * BaseSpeed * ScaledTime.fixedDeltaTime);
            Body.transform.up = direction;
        }
    }
    public override void AbsoluteMove(Vector2 destination, bool look_at = false)
    {
        var previous = Body.position;
        Body.MovePosition(destination);
        if (look_at)
        {
            var dir_length = (previous - destination).sqrMagnitude;
            if (dir_length > 0.001f)
                Body.transform.up = (previous - destination).normalized;
        }
    }

    // MUST be called on Update
    public override void DodgeTeleport(Vector2 direction)
    {
        if(direction != Vector2.zero)
            t.position = Body.position + direction * BaseDodgeDistance;
    }

    public override void Clamp(Vector4 bounds)
    {
        var p = Body.position;
        var clamped_pos = new Vector2(Mathf.Min(bounds.x, Mathf.Max(p.x, bounds.z)),
                                      Mathf.Min(bounds.y, Mathf.Max(p.y, bounds.w)));
        Body.MovePosition(clamped_pos);
    }

    public override void ClampMovement(Vector2 direction, Vector4 bounds)
    {
        var p = Body.position + direction * BaseSpeed * ScaledTime.fixedDeltaTime;
        var clamped_pos = new Vector2(Mathf.Min(bounds.x, Mathf.Max(p.x, bounds.z)),
                                      Mathf.Min(bounds.y, Mathf.Max(p.y, bounds.w)));

        Body.MovePosition(clamped_pos);
    }

    public override void ClampTeleport(Vector2 direction, Vector4 bounds)
    {
        if (direction == Vector2.zero) return;

        PlayerController.Player.GetComponent<TeleportAnimation>().Spawn(Body.position);

        var p = Body.position + direction * BaseDodgeDistance;
        var clamped_pos = new Vector2(Mathf.Min(bounds.x, Mathf.Max(p.x, bounds.z)),
                                      Mathf.Min(bounds.y, Mathf.Max(p.y, bounds.w)));
        TeleportTriggeredEvent.Invoke();
        t.position = clamped_pos;
    }


}
