using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Movement Behaviors/Sin")]
public class SinMovement : MovementBehavior
{
    public float Phase;
    public float Frequency;
    public float Amplitude;


    public override MovementBehavior RandomBetween(MovementBehavior lower_bound, MovementBehavior upper_bound)
    {
        var lb = (SinMovement)lower_bound;
        var ub = (SinMovement)upper_bound;

        var new_mb = ScriptableObject.CreateInstance(typeof(SinMovement)) as SinMovement;
        new_mb.Phase = Random.Range(lb.Phase, ub.Phase);
        new_mb.Frequency = Random.Range(lb.Frequency, ub.Frequency);
        new_mb.Amplitude = Random.Range(lb.Amplitude, ub.Amplitude);

        return new_mb;
    }


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
            prior.direction = forward;
        }


        float dt = (ScaledTime.time - prior.t);
        var v = prior.direction;
        prior.direction = Rotate(prior.direction, Amplitude * Mathf.Sin(Phase + Frequency * ScaledTime.fixedDeltaTime) * ScaledTime.fixedDeltaTime);
        return prior;
    }
}
