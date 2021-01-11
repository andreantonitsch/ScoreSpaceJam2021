using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Waves/Shooting Descriptor")]
public class ShootingDescriptor : ScriptableObject
{
    public ShotBehavior lower_behavior;
    public ShotBehavior upper_behavior;

    public ShotBehavior GetSample()
    {
        return lower_behavior.RandomBetween(lower_behavior, upper_behavior);
    }
}
