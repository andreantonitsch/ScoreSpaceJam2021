using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SawPath : PathGenerator
{
    public float Length1; //from Start to p2
    public float Length2; //from p2 to p4
    public float Length3; //from p2 to p4
    public int Steps = 5;
    public override List<Vector2> Path(Vector2 path_start, Vector2 path_end, Vector2 position, Vector2 forward)
    {
        var y_base = forward.normalized;
        var x_base = Vector2.Perpendicular(y_base);
        var x_translation = x_base * Length1;
        var y_translation = y_base * Length2;
        var y_translation2 = y_base * Length3;
        var base_p = y_base * Length1 + path_start;
        var points = new List<Vector2>();

        points.Add(path_start);
        var current_p = path_start;
        bool go_right = true;
        int s = 0;
        while (s < Steps)
        {
            var p4 = go_right ? current_p + x_translation : current_p - x_translation;
            go_right = !go_right;

            p4 += y_translation;
            var p5 = p4 + y_translation2;
            current_p = p5;
            points.Add(p4);
            points.Add(p5);

            s++;
        }

        points.Add(current_p + (current_p - points[points.Count-2]));

        knots = points;
        return points;
    }

}
