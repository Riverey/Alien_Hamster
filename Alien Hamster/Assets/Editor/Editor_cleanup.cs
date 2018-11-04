using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Editor_cleanup))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Editor_cleanup myScript = (Editor_cleanup)target;
        if (GUILayout.Button("Clean the inspector"))
        {
            myScript.CleanEditor();
        }
    }
}
