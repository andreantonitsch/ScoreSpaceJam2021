using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SenoidPath : PathGenerator
{
    public float Length1; //from Start to p2
    public float Length2; //from p2 to p4
    public float Length3; //from p2 to p4
    public int Steps = 5;
    public override List<Vector2> Path(Vector2 path_start, Vector2 path_end, Vector2 position, Vector2 forward)
    {
        var y_base = forward.normalized;
        var x_base = Vector2.Perpendicular(y_base);
        var x_translation = x_base * Length2;
        var y_translation = y_base * Length1;
        var y_translation2 = y_base * Length3;
        var base_p = y_base * Length1 + path_start;
        var points = new List<Vector2>();
        points.Add(path_start - y_base);
        points.Add(path_start);


        var p1 = base_p + y_translation;
        var p2 = p1 - x_translation * 0.5f;
        var p3 = p2 + y_translation;
        points.Add(p1);
        points.Add(p2);
        points.Add(p3);
        var current_p = p3;
        bool go_right = true;
        int s = 0;
        while (s < Steps)
        {
            var x_t = go_right ? x_translation/2 :- x_translation/2;
            go_right = !go_right;
            var p4 = current_p + x_t;
            p4 = p4 + y_translation;
            
            var p5 = p4 + y_translation + x_t;
            var p6 = p5 + y_translation2;

            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            current_p = p6;
            s++;
        }

        knots = points;
        return points;
    }

}
