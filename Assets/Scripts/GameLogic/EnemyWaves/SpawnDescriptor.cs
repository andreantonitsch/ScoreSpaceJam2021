using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Waves/Spawn Descriptor")]
public class SpawnDescriptor : ScriptableObject
{
    public Vector3 Position;
    public float   Z_rotation;
}
