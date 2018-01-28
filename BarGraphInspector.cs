using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BarGraph))]
public class BarGraphInspector : Editor
{

    public override void OnInspectorGUI()
    {
        BarGraph barGraph = (BarGraph)target;
        barGraph.Orientation = (OrientationEnum)EditorGUILayout.EnumPopup("Order: ", barGraph.Orientation);

    }
}
