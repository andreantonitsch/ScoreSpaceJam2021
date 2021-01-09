using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MovementType 
{ 
    Simple,
    Clamped,
    Teleport,
    ClampedTeleport
}

[System.Serializable]
public struct MovementData
{
    public Vector4 boundary;
    public Vector2 forward;
    public Vector2 position;
    public GameObject source;
}

[System.Serializable]
public struct MovementPacket 
{
    [SerializeField]
    public float t;
    [SerializeField]
    public Vector2 direction;
    [SerializeField]
    public MovementType type;
    [SerializeField]
    public MovementData data;
}

[System.Serializable]
public class MovementBehavior : ScriptableObject
{

    public virtual MovementPacket FixedApply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2), GameObject source = default(GameObject))
    {
        return prior;
    }

    public virtual MovementPacket Apply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2), GameObject source = default(GameObject))
    {
        return prior;
    }

    public virtual MovementPacket InitPacket(float t, Vector2 forward, Vector2 position, GameObject source)
    {
        var prior = new MovementPacket();
        prior.data = new MovementData();
        prior.t = t;
        prior.data.forward = forward;
        prior.data.position = position;
        prior.data.source = source;
        return prior;

    }

    public virtual MovementBehavior Clone()
    {
        return Instantiate(this);
    }

}
