using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum PathConsumerType
{
    Spline,
    Line
}

[System.Serializable]
public enum PathProviderType
{
    Arc,
    //Snake,
    Loop,
    Saw,
    Forward,
    Line//,
    //Senoid

}
[System.Serializable]
public enum PathMode
{
    One,
    Two,
    Three
}

[System.Serializable]
public struct PathData
{
    public float fval1;
    public float fval2;
    public float fval3;
    public float fval4;
    public Vector2 PathEnd;
    public PathMode Mode;

}

[CreateAssetMenu(menuName = "Enemy Paths/Path Pair")]
public class PathAsset : ScriptableObject
{
    public PathConsumerType PathMover;
    public PathProviderType PathGenerator;
    public PathData GeneratorData;

    public PathConsumer GetMover() 
    {
        switch (PathMover)
        {
            case PathConsumerType.Spline:
                return new SplinePathConsumer();
            case PathConsumerType.Line:
                return new LinePathConsumer();
            default:
                return null;
        }
    }
    public PathGenerator GetGenerator() 
    {
        switch (PathGenerator)
        {
            case PathProviderType.Loop:
                var loop = new LoopPath();
                loop.Length1 = GeneratorData.fval1;
                loop.Length2 = GeneratorData.fval2;
                return loop;
            case PathProviderType.Arc:
                var arc = new ArcPath();
                arc.Length1 = GeneratorData.fval1;
                arc.Length2 = GeneratorData.fval2;
                return arc;
            //case PathProviderType.Snake:
            //    var snek = new SnakePath();
            //    snek.Length1 = GeneratorData.fval1;
            //    snek.Length2 = GeneratorData.fval2;
            //    snek.Steps = (int)GeneratorData.fval3;
            //    return snek;
            case PathProviderType.Saw:
                var saw = new SawPath();
                saw.Length1 = GeneratorData.fval1;
                saw.Length2 = GeneratorData.fval2;
                saw.Length3 = GeneratorData.fval3;
                saw.Steps = (int)GeneratorData.fval4;
                return saw;
            case PathProviderType.Forward:
                var forw = new ForwardPath();
                forw.Length1 = GeneratorData.fval1;
                forw.Length2 = GeneratorData.fval2;
                return forw;
            case PathProviderType.Line:
                var line = new LinePath();
                line.Length1 = GeneratorData.fval1;
                line.Length2 = GeneratorData.fval2;
                return line;
            //case PathProviderType.Senoid:
            //    var sen = new SenoidPath();
            //    sen.Length1 = GeneratorData.fval1;
            //    sen.Length2 = GeneratorData.fval2;
            //    sen.Length3 = GeneratorData.fval3;
            //    sen.Steps = (int)GeneratorData.fval4;
            //    return sen;
            default:
                return null;
        }
    }

}
