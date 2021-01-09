using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Test script = (Test)target;

        if (GUILayout.Button("Test"))
        {
            script.CreatePrefab();

        }


    }

}
#endif