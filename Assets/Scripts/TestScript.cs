using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public PlayerMovement player;
    public PhysicsShipMovement physics;
    public ArcadeShipMovement arcade;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            player.movement_controller = physics;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            player.movement_controller = arcade;
    }
}
