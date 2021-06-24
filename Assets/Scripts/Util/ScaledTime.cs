using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledTime : MonoBehaviour
{

    public static float TimeScale = 1.0f;

    private static float _time;

    public static float deltaTime
    {
        get
        {
            return Time.deltaTime * TimeScale;
        }
    }

    public static float fixedDeltaTime
    {
        get
        {
            return Time.fixedDeltaTime * TimeScale;
        }
    }

    public static float time
    {
        get
        {
            return _time;
        }
    }


    public void FixedUpdate()
    {
        _time += TimeScale * Time.fixedDeltaTime;
    }
}
