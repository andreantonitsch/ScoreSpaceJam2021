using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting Behaviors/Targeted")]
public class TargetedShotBehavior : ShotBehavior
{
    public Transform Target;
    //public int BulletQuantity;
    public float Spacing;
    public float Delay = 0.1f;


    public override ShotBehavior RandomBetween(ShotBehavior lower_bound, ShotBehavior upper_bound)
    {
        var lb = (TargetedShotBehavior)lower_bound;
        var ub = (TargetedShotBehavior)upper_bound;

        var new_sb = ScriptableObject.CreateInstance(typeof(TargetedShotBehavior)) as TargetedShotBehavior;
        new_sb.BulletQuantity = Random.Range(lb.BulletQuantity, ub.BulletQuantity);
        new_sb.Delay = Random.Range(lb.Delay, ub.Delay);
        new_sb.Spacing = Random.Range(lb.Spacing, ub.Spacing);
        //new_sb.Randomize = Random.value > 0.5f;

        return new_sb;
    }

    public override IEnumerable Activate(Vector2 anchor, Vector2 forward, GameObject prefab)
    {
        int i = 0;
        if (LayerMask.GetMask("EnemyProjectile", "Enemy").CompareTo(prefab.layer) > 0)
        {
            if (PlayerController.Player == null) yield return null;
            Target = PlayerController.Player?.transform;
        }

        if(Target != null) { 
            var min = -Spacing * BulletQuantity / 2;
            foreach (var obj in GetObjects(BulletQuantity, prefab))
            {
                i++;
                if (Target == null || Target?.transform == null)
                    break;
                var v = (new Vector2(Target.transform.position.x, Target.transform.position.y) - anchor).normalized;
                var perp = Vector2.Perpendicular(v);

                obj.transform.position = anchor + perp * min + perp * Spacing * i;
                obj.transform.up = v;

                if (Delay != 0)
                    yield return null;
                else
                    yield return new WaitForSeconds(Delay);
            }
        }
    }

}
