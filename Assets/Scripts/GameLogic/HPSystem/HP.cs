using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructor))]
public class HP : InitializableMonoBehavior
{
    public int Max;
    public int Current;

    public Destructor destructor_handler;
    public void Start()
    {
        destructor_handler = GetComponent<Destructor>();
    }

    public override void Init()
    {
        Current = Max;
    }

    public void Take(Damage d) 
    {
        Current -= d.Quantity;
        if (Current <= 0)
            HpZeroed();
    }

    public void Restore(int Quantity)
    {
        Current = Mathf.Min(Current + Quantity, Max);
    }

    public void HpZeroed()
    {
        destructor_handler.Destroy();
    }

}
