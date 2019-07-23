using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;


[CustomEditor(typeof(ProgressListener))]
public class ProgressListenerDrawer : Editor{
    private SerializedProperty _serializedTarget;
    public string[] targetFeatureNames;


    private void GetFeatureNames(){
        var serializedTargetValues= serializedObject.FindProperty("target").objectReferenceValue;
        var targetFeatureNamesList = new List<string>();
        foreach (var variable in serializedTargetValues.GetType().GetProperties()){
            if(variable.PropertyType==typeof(float))
                targetFeatureNamesList.Add(variable.Name);
        }
        targetFeatureNames = targetFeatureNamesList.ToArray();
    }
    
    void OnEnable(){
        _serializedTarget = serializedObject.FindProperty("target");
        GetFeatureNames();
        var foundProperty= serializedObject.FindProperty("progressName");
        index = Array.IndexOf(targetFeatureNames,foundProperty.stringValue);
    }
    private int index;
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        GetFeatureNames();
        index = EditorGUILayout.Popup(
            "Feature:",
            index,
            targetFeatureNames);
        var foundProperty= serializedObject.FindProperty("progressName");
        foundProperty.stringValue = (targetFeatureNames[index]);
        foundProperty.serializedObject.ApplyModifiedProperties();
    
    }
}