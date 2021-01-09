using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    public bool Poolable;
    public void Destroy()
    {
        if (Poolable)
            //ObjectPool.Despawn(gameObject);
            Destroy(gameObject);
        else
            Destroy(gameObject);
    }
}
