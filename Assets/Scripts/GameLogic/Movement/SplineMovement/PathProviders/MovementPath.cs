using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PathGenerator
{
    public List<Vector2> knots;
    public virtual List<Vector2> Path(Vector2 path_start, Vector2 relative_path_end, Vector2 position, Vector2 forward) { return null; }

    public Vector2 RelativeDirectionalPathEnd(Vector2 relative_path_end, Vector2 up, Vector2 perp)
    {
        return (perp * relative_path_end.x) + (up * relative_path_end.y);
    }
    public Vector2 AbsolutePathEnd(Vector2 relative_path_end, Vector2 initial_position)
    {
        return relative_path_end - initial_position;
    }

}
