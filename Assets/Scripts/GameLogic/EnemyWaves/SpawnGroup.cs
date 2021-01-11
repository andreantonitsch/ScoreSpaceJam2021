using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Spawn Group")]
public class SpawnGroup : ScriptableObject
{
    public List<SpawnDescriptor> spawn_descriptors;
}
