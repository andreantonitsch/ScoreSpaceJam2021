using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoopPath : PathGenerator
{
    public float Length1; //from Start to p2
    public float Length2; //from p2 to p4
    
    public override List<Vector2> Path(Vector2 path_start, Vector2 relative_path_end, Vector2 position, Vector2 forward)
    {
        var n_forw = forward.normalized;
        var perp_forwd = Vector2.Perpendicular(n_forw);
        var perp_scaled = perp_forwd * (Length2 / 2);
        var base_p = n_forw * Length1 + path_start;
        var points = new List<Vector2>();
        points.Add(path_start - n_forw);
        points.Add(path_start);
        
        var p1 = base_p - perp_scaled;
        var p2 = base_p + n_forw * Length1;
        var p3 = base_p + perp_scaled;
        points.Add(p1);
        points.Add(p2);
        points.Add(p3);

        points.Add(relative_path_end); 
        points.Add(relative_path_end + (relative_path_end - p2).normalized);
        knots = points;
        return points;
    }

}
