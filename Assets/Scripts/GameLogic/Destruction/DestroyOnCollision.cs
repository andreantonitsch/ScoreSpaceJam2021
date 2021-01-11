using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructor))]
public class DestroyOnCollision : InitializableMonoBehavior
{
    public int CollisionCount = 1;
    public int current_collisions = 0;
    public Destructor d_handler;

    public bool BoundaryOnly = false;

    public override void Init()
    {
        base.Init();
        current_collisions = 0;
    }

    void Start()
    {
        d_handler = GetComponent<Destructor>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (BoundaryOnly)
        {
            if (collision.tag == "Boundary")
                current_collisions += 1;
        }
        else
        {
            current_collisions += 1;
        }

        if (CollisionCount <= current_collisions)
            d_handler?.Destroy();
    }
}
