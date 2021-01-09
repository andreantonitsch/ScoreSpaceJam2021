using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArcadeMovement))]
public class MovementPattern : MonoBehaviour
{
    public ArcadeMovement movement_controller;
    [SerializeField]
    public List<MovementBehavior> behaviours;

    public void Start()
    {
        movement_controller = GetComponent<ArcadeMovement>();
    }

    public void AddBehaviour(MovementBehavior mb)
    {
        behaviours.Add(mb);
    }

    public void Update()
    {
        int b_count = behaviours.Count;
        if (b_count == 0)
            return;

        MovementPacket packet = behaviours[0].Apply(new MovementPacket(), true, ScaledTime.time, movement_controller.transform.up) ;

        for (int i = 1; i < b_count; i++)
        {
            packet = behaviours[i].Apply(packet);
        }

        switch (packet.type)

        {
            case MovementType.Clamped:
                Debug.Log("Clamped movement is Fixed ONLY");
                break;
            case MovementType.Simple:
                Debug.Log("Simple movement is Fixed ONLY");
                break;
            case MovementType.Teleport:
                movement_controller.DodgeTeleport(packet.direction);
                break;
            case MovementType.ClampedTeleport:
                movement_controller.ClampTeleport(packet.direction, packet.data.boundary);
                break;
        }

    }

    public void FixedUpdate()
    {
        int b_count = behaviours.Count;
        if (b_count == 0)
            return;

        MovementPacket packet = behaviours[0].FixedApply(new MovementPacket(), true, ScaledTime.time, movement_controller.transform.up);

        for (int i = 1; i < b_count; i++)
        {
            packet = behaviours[i].FixedApply(packet);
        }

        switch (packet.type)

        {
            case MovementType.Clamped:
                movement_controller.ClampMovement(packet.direction, packet.data.boundary);
                break;
            case MovementType.Simple:
                movement_controller.Move(packet.direction);
                break;
            case MovementType.Teleport:
                Debug.Log("Teleports are is Updated ONLY");
                break;
            case MovementType.ClampedTeleport:
                Debug.Log("Teleports are is Updated ONLY");
                break;
        }
    }


}


