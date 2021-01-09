using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StraightMovement : MovementBehavior
{
    public float Speed;
    public float Accel;
    float init_t;
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

        if (init_t == 0) init_t = prior.t;
        float dt = (init_t - ScaledTime.time);

        prior.direction += forward * (Speed + Accel*dt);
        return prior;
    }
}
