using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EasingFunctions
{
    Linear,
    Parabolic,
    GainSlow,
    GainFast,
    PowerSlow,
    PowerMed,
    PowerFast,
    ExpStepSlow,
    ExpStepFast,
    Pulse

}

[RequireComponent(typeof(ArcadeMovement))]
public class SplineFollowerComponent : InitializableMonoBehavior
{
    ArcadeMovement am;
    List<PathConsumer> consumers = new List<PathConsumer>();
    List<PathGenerator> generators = new List<PathGenerator>();

    public float StartTime;
    public Vector2 StartPosition;
    public float Speed;
    public List<float> PathLengths;
    public bool LookAt = true;
    public bool Initialized;

    public bool LengthNormalized = true;

    public EasingFunctions mode;

    public override void Init()
    {
        base.Init();
        am = GetComponent<ArcadeMovement>();
        StartTime = ScaledTime.time;
        StartPosition = am.Body.position;
        Initialized = true;
    }

    public void FixedUpdate()
    {
        if (Initialized)
        {
            float t;
            if (LengthNormalized)
                t = (ScaledTime.time - StartTime) / Speed ;
            else
                t = ((ScaledTime.time - StartTime) * Speed) / PathLengths[0];

            t = PathFunction(t, mode);

            var p = Vector2.zero;
            foreach(var consumer in consumers)
                p += consumer.FollowPath(t);
            p /= consumers.Count;

            if(!float.IsNaN(p.x) && !float.IsNaN(p.y))
                am.AbsoluteMove(p + StartPosition, true);
        }
    }

    public void InitializeMovement(PathAsset asset)
    {
        var generator = asset.GetGenerator();
        var consumer = asset.GetMover();
        consumers.Add(consumer);
        generators.Add(generator);
        var p_end = asset.GeneratorData.PathEnd;

        var path = generator.Path(Vector2.zero, p_end,  StartPosition, transform.up) ;
        consumer.FollowPath(0.0f, path);
        //var pts = (consumer)?.PathPoints;
        PathLengths.Add((consumer).PathLength);
    }

    public void InitializeMovement(PathAsset asset, Vector2 p_end)
    {
        var generator = asset.GetGenerator();
        var consumer = asset.GetMover();
        consumers.Add(consumer);
        generators.Add(generator);

        var path = generator.Path(Vector2.zero, p_end, StartPosition, transform.up);
        consumer.FollowPath(0.0f, path);
        //var pts = (consumer)?.PathPoints;
        PathLengths.Add((consumer).PathLength);
    }

    public void InitializeMovement(PathConsumer _consumer, Vector2 PathEnd)
    {
        //consumer = _consumer;
        //generator = _generator;

    }

    public void OnDrawGizmosSelected()
    {
        if (consumers.Count != 0)
        {
            var pos = StartPosition;
            for (int i = 0; i < consumers.Count; i++)
            {
                var path = consumers[i].PathPoints;
                var knots = generators[i].knots;
                Gizmos.color = Color.white;
                for (int j = 0; j < path.Count - 1; j++)
                {
                    Gizmos.DrawLine(path[j] + pos, path[j + 1] + pos);
                }
                Gizmos.color = Color.red;
                for (int j = 0; j < knots.Count - 1; j++)
                {
                    Gizmos.DrawLine(knots[j] + pos, knots[j + 1] + pos);
                }
            }
            var last_pos = Vector2.zero;
            Gizmos.color = Color.blue;
            for (float j = 0.0f; j < 1.0f; j+=0.02f)
            {
                var p = Vector2.zero;
                for (int i = 0; i < consumers.Count; i++)
                {
                    p += consumers[i].FollowPath(j);
                }
                p /= consumers.Count;
                Gizmos.DrawLine(last_pos + pos, p + pos);
                last_pos = p;
            }

        }
    }
    public float PathFunction(
    float t, EasingFunctions mode)
    {
        return mode switch
        {
            EasingFunctions.Linear => ShapingFunctions.Linear(t, 1.0f),
            EasingFunctions.Parabolic => ShapingFunctions.Parabola(t, 2.0f),
            EasingFunctions.GainSlow => ShapingFunctions.Gain(t, 1.5f),
            EasingFunctions.GainFast => ShapingFunctions.Gain(t, 0.5f),
            EasingFunctions.PowerSlow => ShapingFunctions.PowerCurve(t, 1.5f, 1.0f),
            EasingFunctions.PowerMed => ShapingFunctions.PowerCurve(t, 1.0f, 3.0f),
            EasingFunctions.PowerFast => ShapingFunctions.PowerCurve(t, 0.5f, 3.5f),
            EasingFunctions.ExpStepSlow => 1 - ShapingFunctions.ExpStep(t, 1.5f, 1.9f),
            EasingFunctions.ExpStepFast => 1 - ShapingFunctions.ExpStep(t, 1.7f, 4.0f),
            EasingFunctions.Pulse => ShapingFunctions.CubicPulse(t, 0.5f, 1.0f),
            _ => 0.0f,
        };
    }
}