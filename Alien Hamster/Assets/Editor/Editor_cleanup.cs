using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Editor_cleanup_act))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Editor_cleanup_act myScript = (Editor_cleanup_act)target;
        if (GUILayout.Button("Clean the inspector"))
        {
            myScript.CleanEditor();
        }
    }
}
