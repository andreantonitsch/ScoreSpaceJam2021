using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(MovementPattern))]
public class MovementPatternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovementPattern script = (MovementPattern)target;

        if (GUILayout.Button("Add PlayerMovement"))
        {
            var player = ScriptableObject.CreateInstance<PlayerMovement>();
            script.behaviours.Add(player);

        }
        if (GUILayout.Button("Add Curver"))
        {
            var player = ScriptableObject.CreateInstance<CurverMovement>();
            script.behaviours.Add(player);
        }
        if (GUILayout.Button("Add Straight"))
        {
            var player = ScriptableObject.CreateInstance<StraightMovement>();
            script.behaviours.Add(player);
        }

    }

}
#endif