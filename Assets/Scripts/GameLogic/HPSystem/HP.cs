using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Destructor))]
public class HP : InitializableMonoBehavior
{
    public delegate void HPChangedEventHandler(int new_quantity);
    public event HPChangedEventHandler HPChangedEvent;

    public delegate void HPZeroedEventHandler();
    public event HPZeroedEventHandler HPZeroedEvent;

    public int Max;
    private int current;

    public Destructor destructor_handler;

    public int Current { get => current; 
        set 
        {
            current = value;
            HPChangedEvent?.Invoke(current);
        } 
    }

    public void Start()
    {
        destructor_handler = GetComponent<Destructor>();
        Init();
    }

    public override void Init()
    {
        Current = Max;
    }

    public void Take(Damage d) 
    {
        Current = Current - d.Quantity;
        
        if (Current <= 0)
            HpZeroed();
    }

    public void Restore(int Quantity)
    {
        Current = Mathf.Min(Current + Quantity, Max);
    }

    public void HpZeroed()
    {
        HPZeroedEvent?.Invoke();
        destructor_handler.Destroy();
    }

}
