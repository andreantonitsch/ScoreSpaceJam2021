using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructor))]
public class TimedDestruction : InitializableMonoBehavior
{

    public float Timer = 2.0f;
    private float timer = 0.0f;
    public Destructor d_handler;

    public override void Init()
    {
        base.Init();
        timer = Timer;
    }

    void Start()
    {
        d_handler = GetComponent<Destructor>();
        Init();
    }


    private void FixedUpdate()
    {
        if (timer < 0)
            d_handler.Destroy();

        timer -= ScaledTime.fixedDeltaTime;
    }
}
