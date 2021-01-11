using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shooting Behaviors/Forward")]
public class ForwardShotBehavior : ShotBehavior
{
    //public int BulletQuantity;
    public float Spacing;
    public float Delay = 0.1f;
    public bool Randomize = false;

    public override ShotBehavior RandomBetween(ShotBehavior lower_bound, ShotBehavior upper_bound)
    {
        var lb = (ForwardShotBehavior)lower_bound;
        var ub = (ForwardShotBehavior)upper_bound;

        var new_sb = ScriptableObject.CreateInstance(typeof(ForwardShotBehavior)) as ForwardShotBehavior;
        new_sb.BulletQuantity = Random.Range(lb.BulletQuantity, ub.BulletQuantity);
        new_sb.Delay = Random.Range(lb.Delay, ub.Delay);
        new_sb.Spacing = Random.Range(lb.Spacing, ub.Spacing);
        new_sb.Randomize = Random.value > 0.5f;

        return new_sb;
    }


    public override IEnumerable Activate(Vector2 anchor, Vector2 forward, GameObject prefab)
    {
        int i = 0; 
        var v = forward;
        var perp = Vector2.Perpendicular(v);

        var min = -Spacing * BulletQuantity / 2;
        foreach (var obj in GetObjects(BulletQuantity, prefab))
        {
            if (Randomize)
                i = (int)(Random.value * BulletQuantity);
            obj.transform.position = anchor + perp * min + perp * Spacing * i;
            obj.transform.up = v;
            
            i++;    
            if (Delay == 0)
                yield return null;
            else
                yield return new WaitForSeconds(Delay);
        }

    }

}
