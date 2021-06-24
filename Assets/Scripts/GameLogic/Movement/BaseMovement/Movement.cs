using UnityEngine;

public class Movement : MonoBehaviour
{
    public virtual void Move(Vector2 direction) { }
    public virtual void AbsoluteMove(Vector2 destination, bool look_at) { }
    public virtual void DodgeTeleport(Vector2 direction) { }
    public virtual void Clamp(Vector4 bounds) { }
    public virtual void ClampMovement(Vector2 direction, Vector4 bounds) { }
    public virtual void ClampTeleport(Vector2 direction, Vector4 bounds) { }
}
