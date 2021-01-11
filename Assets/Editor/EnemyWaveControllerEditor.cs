using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyWaveController))]
public class EnemyWaveControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyWaveController script = (EnemyWaveController)target;

        if (GUILayout.Button("Wave Start"))
        {
            script.Ready = true;
        }

    }

}
#endif