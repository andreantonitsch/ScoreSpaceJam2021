using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  ShotBehavior : ScriptableObject
{
    public int BulletQuantity;

    public virtual ShotBehavior RandomBetween(ShotBehavior lower_bound, ShotBehavior upper_bound) { return null; }


    public virtual IEnumerable Activate(Vector2 anchor, Vector2 forward, GameObject prefab)
    {
        yield return  null;
    }

    public IEnumerable<GameObject> GetObjects(int quantity, GameObject projectile_prefab)
    {
        for (int i = 0; i < quantity; i++)
        {

            //var obj = ObjectPool.Spawn(projectile_prefab, Vector3.zero, Quaternion.identity);
            var obj = Instantiate(projectile_prefab, Vector3.zero, Quaternion.identity);
            var imbs = obj.GetComponents<InitializableMonoBehavior>();

            foreach (var imb in imbs)
            {
                imb.Init();
                imb.gameObject.SetActive(true);
            }
           yield return obj;
        }
    }
}
