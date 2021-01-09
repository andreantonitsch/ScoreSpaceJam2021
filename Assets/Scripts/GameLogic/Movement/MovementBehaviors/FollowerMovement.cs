using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowerMovement : MovementBehavior
{
    public float TurnSpeed;
    public Transform Target;
    public float init_t;

    public static Vector2 Rotate (Vector2 v, float rad)
    {
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }

    public override MovementPacket Apply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2))
    {
        if (init)
        {
            prior = InitPacket(t, forward, position);
            prior.type = MovementType.Teleport;
        }

        return prior;
    }

    public override MovementPacket FixedApply(MovementPacket prior, bool init = false, float t = 0, Vector2 forward = default(Vector2), Vector2 position = default(Vector2))
    {
        if (init)
        {
            prior = InitPacket(t, forward, position);
            prior.type = MovementType.Simple;
        }

        if (init_t == 0)
            init_t = prior.t;
        var v = prior.direction;
        float dt = (init_t - ScaledTime.time);
        var tt = Target.transform.position;
        float angle = Vector2.Angle(new Vector2(tt.x, tt.y) - prior.data.position, prior.direction);

        prior.direction = Rotate(prior.direction, TurnSpeed * dt);
        return prior;
    }
}
