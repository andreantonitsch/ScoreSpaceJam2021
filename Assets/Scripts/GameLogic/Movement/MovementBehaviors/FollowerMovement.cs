using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "Movement Behaviors/Follower")]
public class FollowerMovement : MovementBehavior
{
    public float TurnSpeed;

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

        int i = 0;

        Transform target;
        if (LayerMask.GetMask("Enemy", "EnemyProjectile").CompareTo(prior.data.source.layer) > 0)
        {
            if (PlayerController.Player != null)
            {
                target = PlayerController.Player.transform;
            }
            else { return prior; }
        }
        else {   return prior; }

        var v = prior.direction . normalized;
        float dt = (ScaledTime.time - prior.t);
        var tt = target.transform.position;
        var v2 = (new Vector2(tt.x, tt.y) - prior.data.position).normalized;
        //float angle = -Vector2.SignedAngle(new Vector2(tt.x, tt.y) - prior.data.position, v) / (Mathf.PI * 2);

        //prior.direction = Rotate(v, TurnSpeed * angle * dt);
        prior.direction = Vector2.Lerp(v, v2,  TurnSpeed  * dt) * prior.direction.magnitude;


        return prior;
    }
}
