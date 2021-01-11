using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Shooting Group")]
public class ShootingGroup : ScriptableObject 
{
    public List<ShootingDescriptor> shooting_descriptors;
}
