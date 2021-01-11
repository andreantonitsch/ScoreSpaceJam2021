using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Movement Behaviors/Straight")]
public class StraightMovement : MovementBehavior
{
    public float Speed;
    public float Accel;
    public override MovementBehavior RandomBetween(MovementBehavior lower_bound, MovementBehavior upper_bound)
    {
        var lb = (StraightMovement)lower_bound;
        var ub = (StraightMovement)upper_bound;

        var new_mb = ScriptableObject.CreateInstance(typeof(StraightMovement)) as StraightMovement;
        new_mb.Speed = Random.Range(lb.Speed, ub.Speed);
        new_mb.Accel = Random.Range(lb.Accel, ub.Accel);

        return new_mb;
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

        float dt = (ScaledTime.time - prior.t);
        prior.direction += forward * (Speed + Accel*dt);
        return prior;
    }
}
