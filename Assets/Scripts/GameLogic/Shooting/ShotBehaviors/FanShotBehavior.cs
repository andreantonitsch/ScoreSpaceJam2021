using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shooting Behaviors/Fan")]
public class FanShotBehavior : ShotBehavior
{
    //public int BulletQuantity;
    public Vector2 AngleRange;

    public override ShotBehavior RandomBetween(ShotBehavior lower_bound, ShotBehavior upper_bound)
    {
        var lb = (FanShotBehavior)lower_bound;
        var ub = (FanShotBehavior)upper_bound;

        var new_sb = ScriptableObject.CreateInstance(typeof(FanShotBehavior)) as FanShotBehavior;
        new_sb.BulletQuantity = Random.Range(lb.BulletQuantity, ub.BulletQuantity);
        new_sb.AngleRange = new Vector2(Random.Range(lb.AngleRange.x, ub.AngleRange.x), Random.Range(lb.AngleRange.y, ub.AngleRange.y));

        return new_sb;
    }

    public static Vector2 Rotate(Vector2 v, float rad)
    {
        
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }

    public override IEnumerable Activate(Vector2 anchor, Vector2 forward, GameObject prefab)
    {
        int i = 0;
        foreach(var obj in GetObjects(BulletQuantity, prefab))
        {
            i++;
            var angle = Mathf.Lerp(AngleRange.x, AngleRange.y, (float)i / (float)BulletQuantity);
           
            var r = Rotate(forward, angle);
            obj.transform.up = Rotate(forward, angle);
            obj.transform.position = anchor;



            yield return null;
        }

    }


}
