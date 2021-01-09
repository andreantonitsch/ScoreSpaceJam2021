using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Movement Behaviors/Straight")]
public class StraightMovement : MovementBehavior
{
    public float Speed;
    public float Accel;
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

        float dt = (ScaledTime.time - prior.t);
        prior.direction += forward * (Speed + Accel*dt);
        return prior;
    }
}
