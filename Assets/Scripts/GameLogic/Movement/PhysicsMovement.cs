using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsMovement : Movement
{
    public Rigidbody2D bo;
    public Transform t;
    public float Speed;
    public float DodgeDistance;

    public void Start()
    {
        bo = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
    }

    public override void Move(Vector2 direction)
    {
        bo.AddForce(direction * Speed * ScaledTime.fixedDeltaTime);
    }

    // MUST be called on Update
    public override void DodgeTeleport(Vector2 direction)
    {
        t.position = bo.position + direction * DodgeDistance;
    }

}
