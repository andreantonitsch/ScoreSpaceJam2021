using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shooting Behaviors/Delayed Fan")]
public class DelayedFanShotBehavior : ShotBehavior
{
    //public int BulletQuantity;
    public Vector2 AngleRange;
    public float Delay = 0.1f;
    public float ArcDistance = 0.0f;
    public bool Randomize;

    public override ShotBehavior RandomBetween(ShotBehavior lower_bound, ShotBehavior upper_bound)
    {
        var lb = (DelayedFanShotBehavior)lower_bound;
        var ub = (DelayedFanShotBehavior)upper_bound;

        var new_sb = ScriptableObject.CreateInstance(typeof(DelayedFanShotBehavior)) as DelayedFanShotBehavior;
        new_sb.BulletQuantity = Random.Range(lb.BulletQuantity, ub.BulletQuantity);
        new_sb.AngleRange = new Vector2 (Random.Range(lb.AngleRange.x, ub.AngleRange.x), Random.Range(lb.AngleRange.y, ub.AngleRange.y));
        new_sb.Delay = Random.Range(lb.Delay, ub.Delay);
        new_sb.ArcDistance = Random.Range(lb.ArcDistance, ub.ArcDistance);
        new_sb.Randomize = Random.value > 0.5f;

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
            if (Randomize)
                i = (int)(Random.value * BulletQuantity);
            var angle = Mathf.Lerp(AngleRange.x, AngleRange.y, (float)i / (float)BulletQuantity);


            var r = Rotate(forward, angle);
            var rb = obj.GetComponent<Rigidbody2D>();
            obj.transform.up = Rotate(forward, angle);
            obj.transform.position = anchor + r * ArcDistance;


            if (Delay != 0)
                yield return new WaitForSeconds(Delay);
            else
                yield return null;
        }

    }
}
