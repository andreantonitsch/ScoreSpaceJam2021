using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShootingPattern : InitializableMonoBehavior
{
    private IEnumerator<GameObject> behavior;
    public ShotBehavior shot;
    public ShotTrigger trigger;
    public float Cooldown;
    public float cooldown_timer;
    public GameObject ProjectilePrefab;

    public override void Init()  
    {
        Comission();
        cooldown_timer = 0.0f;
    }

    public void Comission()
    {
        trigger.Sub(this);
        trigger.Init();
    }

    public void FixedUpdate()
    {
        cooldown_timer -= ScaledTime.fixedDeltaTime;
    }

    

    public void Pattern() 
    {
        //if (behavior != null && behavior.MoveNext())
        //{
        //    var obj = behavior.Current;
        //}
        //else
        //{
        //    if (behavior != null)
        //        behavior.Dispose();
        //    behavior = shot.Activate(transform.position, transform.forward, ProjectilePrefab).GetEnumerator() ;
        //}

        StartCoroutine(shot.Activate(transform.position, transform.up, ProjectilePrefab).GetEnumerator());
    }

    public void Shoot() 
    {
        if (cooldown_timer <= 0.0f)
        {
            Pattern();
            cooldown_timer = Cooldown;
        }
    }
}
