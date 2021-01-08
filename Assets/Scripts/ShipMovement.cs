using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public virtual void Move(Vector2 direction) { }
    public virtual void MoveTowards(Vector2 direction) { }
    public virtual void DodgeTeleport(Vector2 direction) { }
}
