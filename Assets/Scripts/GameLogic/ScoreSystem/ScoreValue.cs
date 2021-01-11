using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreValue : MonoBehaviour
{
    public int ScoreMultiplier;
    public ScoreController sc;
    public HP hp_component;

    public delegate void AddScoreEventHandler(int multiplier);
    public event AddScoreEventHandler ScoreChangedEvent;

    public void HPZeroed()
    {
        ScoreChangedEvent?.Invoke(ScoreMultiplier);
    }

    public void Start()
    {
        sc = ScoreController.Instance;
        if (sc == null) { return; }
        hp_component = GetComponent<HP>();
        hp_component.HPZeroedEvent += HPZeroed;
        sc.SubscribeScoreObject(this);

    }

}
