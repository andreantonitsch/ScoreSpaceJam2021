using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArcPath : PathGenerator
{
    public float Length1; //from Start to p1
    public float Length2; //from End to p2
    
    public override List<Vector2> Path(Vector2 path_start, Vector2 relative_path_end, Vector2 position, Vector2 forward)
    {
        var abs_path_end = AbsolutePathEnd(relative_path_end, position);
        var side = Mathf.Sign(Vector3.Cross(forward, abs_path_end).z);

        var f_norm = forward.normalized;
        var f_scaled = f_norm * (Length1);

        var p_norm = Vector2.Perpendicular(f_norm).normalized * side;
        var p_scaled = p_norm * Length2;

        var points = new List<Vector2>();
        points.Add(path_start - f_norm);
        points.Add(path_start);

        var p1 = path_start + f_scaled;
        var p2 = abs_path_end - p_scaled;
        points.Add(p1);
        points.Add(p2);

        points.Add(abs_path_end); 
        points.Add(abs_path_end - p_norm);

        knots = points;
        return points;
    }

}
