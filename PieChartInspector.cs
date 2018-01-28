using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
[CustomEditor(typeof(PieChart))]
public class PieChartInspector : Editor {
    public string outcomeText;

    public override void OnInspectorGUI()
    {
        PieChart piechart = (PieChart)target;
        if (GUILayout.Button("Regenerate Chart"))
        {
            piechart.drawChart();
        }
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Rotation Offset", EditorStyles.boldLabel);
        piechart.rotationOffset = EditorGUILayout.Slider(piechart.rotationOffset, -360, 360);
        GUILayout.EndHorizontal();
        piechart.order = (orderEnum)EditorGUILayout.EnumPopup("Order: ", piechart.order);
        GUILayout.Space(10);
        EditorGUILayout.LabelField(new GUIContent("Value Text", "Displays text which shows information about the text, use codes <%> Percentage, <V> Value, <N> Name in the string to decode"));
        piechart.valueDisplay = EditorGUILayout.TextField("", piechart.valueDisplay);
        if (piechart.valueDisplay != "")
        {
            EditorGUILayout.LabelField("Outcome: " + outcomeText);
            GUILayout.Space(10);
            piechart.textPositonOffset = EditorGUILayout.Vector3Field("Text Position Offset", piechart.textPositonOffset);
            piechart.textRotationOffset = EditorGUILayout.Vector3Field("Text Rotation Offset", piechart.textRotationOffset);
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Triggers", EditorStyles.boldLabel);
        piechart.Triggers = EditorGUILayout.Toggle(piechart.Triggers);
        GUILayout.EndHorizontal();
        if (!piechart.Triggers)
        {
            piechart.TriggerOnHover = false;
            piechart.TriggerOnClick = false;
        }
        if (piechart.Triggers)
        {
            GUILayout.BeginHorizontal();
            piechart.TriggerOnClick = EditorGUILayout.Toggle(piechart.TriggerOnClick);
            EditorGUILayout.LabelField("On Click", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            if (piechart.TriggerOnClick)
            {
                SerializedProperty clickMethod = serializedObject.FindProperty("clickMethod");
                EditorGUIUtility.labelWidth = 25;
                EditorGUIUtility.fieldWidth = 50;
                EditorGUILayout.PropertyField(clickMethod);
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.BeginHorizontal();
            piechart.TriggerOnHover = EditorGUILayout.Toggle(piechart.TriggerOnHover);
            EditorGUILayout.LabelField("On Hover", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            if (piechart.TriggerOnHover)
            {
                SerializedProperty hoverMethod = serializedObject.FindProperty("hoverMethod");
                EditorGUIUtility.labelWidth = 25;
                EditorGUIUtility.fieldWidth = 50;
                EditorGUILayout.PropertyField(hoverMethod);
                serializedObject.ApplyModifiedProperties();
            }
        }
        GUILayout.Space(20);
        //List
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Values", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Overall: " + piechart.overallValue, EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        for (int i = 0; i < piechart.dataList.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            piechart.dataList[i].name = EditorGUILayout.TextField("Name:", piechart.dataList[i].name);
            GUILayout.BeginHorizontal();
            piechart.dataList[i].value = EditorGUILayout.FloatField("Value:", piechart.dataList[i].value);
            EditorGUILayout.LabelField("By Percentage: %" + ((piechart.dataList[i].value / piechart.overallValue) * 100).ToString("F2"));
            GUILayout.EndHorizontal();
            piechart.dataList[i].color = EditorGUILayout.ColorField("Color:", piechart.dataList[i].color);
            if (GUILayout.Button("Remove"))
                piechart.dataList.Remove(piechart.dataList[i]);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            this.Repaint();
        }
        if (GUILayout.Button("Add Value"))
        {
            piechart.addData();
        }

        GUILayout.Space(10);
        piechart.circle = (Sprite)EditorGUILayout.ObjectField("Circle Texture", piechart.circle, typeof(Sprite), true);

        if (GUI.changed)
        {
            piechart.drawChart();
            foreach (PieChart.data da in piechart.dataList)
            {
                if (da.value <= 0)
                {
                    da.value = .1f;

                }
            }

            outcomeText = piechart.decryptText(piechart.valueDisplay);
            serializedObject.ApplyModifiedProperties();
        }
    }
  
}
