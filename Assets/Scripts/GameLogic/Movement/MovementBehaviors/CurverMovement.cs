using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Movement Behaviors/Curver")]
public class CurverMovement : MovementBehavior
{
    public float RadPerSec;
    public float AngularAccel;

    public static Vector2 Rotate (Vector2 v, float rad)
    {
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }

    public override MovementPacket Apply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2), GameObject source = default(GameObject))
    {
        if (init)
        {
            prior = InitPacket(t, forward, position, source);
            prior.type = MovementType.Teleport;
        }

        return prior;
    }

    public override MovementPacket FixedApply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2), GameObject source = default(GameObject))
    {
        if (init)
        {
            prior = InitPacket(t, forward, position, source);
            prior.type = MovementType.Simple;
        }

        var v = prior.direction;

        float dt = (ScaledTime.time - prior.t);

        prior.direction = Rotate(prior.direction, (RadPerSec + AngularAccel * dt)*dt  );
        return prior;
    }
}
