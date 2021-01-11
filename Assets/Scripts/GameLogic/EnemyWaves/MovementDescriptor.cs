using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Waves/Movement Descriptor")]
public class MovementDescriptor : ScriptableObject
{
    public MovementBehavior lower_behavior;
    public MovementBehavior upper_behavior;

    public MovementBehavior GetSample()
    {
        return lower_behavior.RandomBetween(lower_behavior, upper_behavior);
    }
}
