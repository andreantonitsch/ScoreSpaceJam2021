using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSequencer : MonoBehaviour
{
    public List<EnemyEncounter> encounters;
    public float Cadence; // Parameter to control difficulty
                          // based on time and player stats.
}
