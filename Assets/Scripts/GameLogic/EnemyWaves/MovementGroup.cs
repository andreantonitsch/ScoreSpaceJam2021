using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Movement Group")]
public class MovementGroup : ScriptableObject
{
    public List<MovementDescriptor> movement_descriptors;
}
