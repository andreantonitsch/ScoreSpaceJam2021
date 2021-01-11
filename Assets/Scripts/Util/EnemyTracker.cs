using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public static List<EnemyTracker> tracked_enemies = new List<EnemyTracker>();

    public void Start()
    {
        EnemyTracker.tracked_enemies.Add(this);
    }

    public void OnDestroy()
    {
        EnemyTracker.tracked_enemies.Remove(this);
    }

}
