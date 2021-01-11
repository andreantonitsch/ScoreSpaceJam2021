using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Enemy Descriptor")]
public class EnemyDescriptor : ScriptableObject
{
    public SpawnGroup spawn_group; // check
    public GameObject base_prefab; // check 
    public MovementGroup movement_group; //check
    public ShootingGroup shooting_group; //check
    public int MovementElements = 1; //check
    public Vector2Int QuantityRange = new Vector2Int(1, 5); //check
    public Vector2Int BulletQuiantity = new Vector2Int(1, 5); //check
    // Arcade Movement behavior
    public Vector2 BaseSpeedRange = new Vector2(1f, 5f); //check

    // Timed Shot Behavior
    public Vector2 ShootingCooldownRange = new Vector2(1f, 5f); //check
    public Vector2 BurstIntervalRange = new Vector2(0.1f, 1); //check
    public Vector2 BurstDurationRange = new Vector2(0, 1); //check

}
