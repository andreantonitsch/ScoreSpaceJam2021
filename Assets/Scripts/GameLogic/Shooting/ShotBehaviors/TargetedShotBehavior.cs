using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting Behaviors/Targeted")]
public class TargetedShotBehavior : ShotBehavior
{
    public Transform Target;
    public int BulletQuantity;
    public float Spacing;
    public float Delay = 0.1f;

    public override IEnumerable Activate(Vector2 anchor, Vector2 forward, GameObject prefab)
    {
        int i = 0;
        if (LayerMask.GetMask("EnemyProjectile", "Enemy").CompareTo(prefab.layer) > 0)
        {
            Target = PlayerController.Player.transform;
        }

        var min = Spacing * BulletQuantity / 2;
        foreach (var obj in GetObjects(BulletQuantity, prefab))
        {
            i++;

            var v = new Vector2(Target.transform.position.x, Target.transform.position.y).normalized;
            var perp = Vector2.Perpendicular(v);
            obj.transform.up = v;
            obj.transform.position = anchor + perp * min + perp * Spacing * i;
            if (Delay != 0)
                yield return null;
            else
                yield return new WaitForSeconds(Delay);
        }

    }

}
