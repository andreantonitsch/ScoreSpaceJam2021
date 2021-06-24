using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LinePath : PathGenerator
{
    public float Length1; //from Start to p1
    public float Length2; //from End to p2
    
    public override List<Vector2> Path(Vector2 path_start, Vector2 relative_path_end, Vector2 position, Vector2 forward)
    {
        var abs_end = AbsolutePathEnd(relative_path_end, position);
        var dir = (abs_end).normalized * Length1;

        var points = new List<Vector2>();
        points.Add(path_start - dir);
        points.Add(path_start);

        points.Add(path_start + dir);
        points.Add(path_start + dir + dir);
        points.Add(abs_end);
        points.Add(abs_end + dir +dir );

        knots = points;
        return points;
    }

}
