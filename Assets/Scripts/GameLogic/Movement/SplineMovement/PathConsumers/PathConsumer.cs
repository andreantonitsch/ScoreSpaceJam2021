using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathConsumer
{
    public float PathLength;
    public List<Vector2> PathPoints;
    public List<float> TValues;

    public virtual Vector2 FollowPath(float t, List<Vector2> positions = null) { return Vector3.zero; }

}
