using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Enemy Group")]
public class EnemyGroup : ScriptableObject
{
    public List<EnemyDescriptor> EnemyDescriptors;
}
