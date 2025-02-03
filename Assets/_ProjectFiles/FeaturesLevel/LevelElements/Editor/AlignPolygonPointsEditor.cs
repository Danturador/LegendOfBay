using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AlignPolygonPoints))]
public class AlignPolygonPointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AlignPolygonPoints aligner = (AlignPolygonPoints)target;

        if (GUILayout.Button("Align Points"))
        {
            aligner.AlignPoints();
        }
    }
}