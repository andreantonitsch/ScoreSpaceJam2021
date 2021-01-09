using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Shooting Behaviors/Fan")]
public class FanShotBehavior : ShotBehavior
{
    public int BulletQuantity;
    public Vector2 AngleRange;


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
            var v = obj.transform.up;
            var r = Rotate(forward, angle);
            obj.transform.up = Rotate(forward, angle);
            obj.transform.position = anchor;
            
            yield return null;
        }

    }


}
