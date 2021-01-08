using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsShipMovement : ShipMovement
{
    public Rigidbody2D ship;
    public float Speed;
    public float DodgeDistance;

    public void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }
    public override void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - ship.position).normalized * Speed * ScaledTime.fixedDeltaTime;
        Move(direction);
    }

    public override void Move(Vector2 direction)
    {
        ship.AddForce(/*ship.position +*/ direction * Speed * ScaledTime.fixedDeltaTime);
    }

    public override void DodgeTeleport(Vector2 direction)
    {
        ship.position = ship.position + direction * DodgeDistance;
    }

}
