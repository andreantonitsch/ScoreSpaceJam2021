using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Wave Descriptor")]
public class EnemyWaveDescriptor : ScriptableObject
{
    public bool DurationOnly; //check
    public Vector2 DurationRange; //check
    public EnemyGroup enemies_available; //check

    public ShootingGroup shooting_behaviors; //check

}
