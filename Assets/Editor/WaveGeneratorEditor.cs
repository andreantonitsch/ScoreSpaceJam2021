using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(WaveGenerator))]
public class WaveGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WaveGenerator script = (WaveGenerator)target;

        if (GUILayout.Button("Test Wave Gen"))
        {
            var a = script.GenerateWave(script.WaveDescritors[0]);
            a.transform.parent = script.transform.parent;
        }

    }

}
#endif