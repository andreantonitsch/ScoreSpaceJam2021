using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePathConsumer : PathConsumer
{

    public override Vector2 FollowPath(float t, List<Vector2> positions = null)
    {
        if (positions != null)
            ProcessPath(positions);

        return GetTPosition(t);
    }

    public Vector2 GetTPosition(float t)
    {
        var l_i = 0;
        var u_i = TValues.Count - 1;

        while (l_i < u_i && l_i != (u_i - 1))
        {

            var mid_i = (u_i + l_i) / 2;
            var mid_bound = TValues[mid_i];
            if (mid_bound > t)
            {
                u_i = mid_i;
            }
            else
            {
                l_i = mid_i;
            }

        }

        return InterpolatePos(u_i, l_i, t);
    }

    public Vector2 InterpolatePos(int start_i, int end_i, float t)
    {
        if (start_i < end_i)
        {
            var a = start_i;
            start_i = end_i;
            end_i = start_i;
        }

        var prop = t - TValues[start_i];
        var delta = TValues[end_i] - TValues[start_i];
        if (delta != 0)
            prop /= delta;

        return PathPoints[start_i] * (1.0f - prop) + PathPoints[end_i] * ( prop );
    }

    public void ProcessPath(List<Vector2> knots)
    {
        float total_length = 0;
        PathPoints = knots;

        TValues = new List<float>();
        TValues.Add(0.0f);
        // distance between point i to i+1
        // N points make N-1 segments
        for (int i = 0; i < PathPoints.Count - 1; i++)
        {
            var l = (PathPoints[i + 1] - PathPoints[i]).magnitude;
            total_length += l;
            TValues.Add(total_length);
        }
        for (int i = 0; i < TValues.Count; i++)
        {
            TValues[i] = TValues[i] / total_length;
        }

        PathLength = total_length;


    }

}
