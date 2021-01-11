using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Movement Behaviors/Curver")]
public class CurverMovement : MovementBehavior
{
    public float RadPerSec;
    public float AngularAccel;

    public override MovementBehavior RandomBetween(MovementBehavior lower_bound, MovementBehavior upper_bound) 
    {
        var lb = (CurverMovement)lower_bound;
        var ub = (CurverMovement)upper_bound;

        var new_curver = ScriptableObject.CreateInstance(typeof(CurverMovement)) as CurverMovement;
        new_curver.RadPerSec = Random.Range(lb.RadPerSec, ub.RadPerSec);
        new_curver.AngularAccel = Random.Range(lb.AngularAccel, ub.AngularAccel);

        return new_curver;
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

        var v = prior.direction;

        float dt = (ScaledTime.time - prior.t);
        
        prior.direction = Rotate(prior.direction, (RadPerSec + AngularAccel * dt) * ScaledTime.fixedDeltaTime);
        return prior;
    }
}
