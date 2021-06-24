using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePath : PathGenerator
{
    public float Length1; //forward step
    public float Length2; //sideways step
    public int StepInversion = 1;
    public int Steps = 5;
    
    public override List<Vector2> Path(Vector2 path_start, Vector2 relative_path_end, Vector2 position, Vector2 forward)
    {
        var y_base = forward.normalized;
        var x_base = Vector2.Perpendicular(y_base);
        var x_translation = x_base * Length2;
        var y_translation = y_base * Length1;
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
        while(s < Steps)
        {
            var p4 = go_right ? current_p + x_translation : current_p - x_translation;
            go_right = !go_right;
            var p5 = p4 + y_translation;
            current_p = p5;
            points.Add(p4);
            points.Add(p5);
            s++;
        }

        knots = points;
        return points;
    }
}
