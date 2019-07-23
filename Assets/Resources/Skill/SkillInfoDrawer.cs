using System;
using UnityEditor;
using UnityEngine;

//[CustomPropertyDrawer(typeof(Skill.UnitAdditionalInfo))]
//public class SkillInfoDrawer : PropertyDrawer{
//    public int fieldNum;
//
//    public Skill.TimelineUnit.UnitType unitType;
//    
//    // Start is called before the first frame update
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
//        var LabelWidth = EditorGUIUtility.labelWidth;
//        var targetSkill = property.serializedObject.targetObject as Skill;
//        var propertyPath = property.propertyPath;
//        var lastIndexOf = propertyPath.LastIndexOf('.');
//        if (lastIndexOf == -1){
//            Debug.LogError("Wrong path of object " + property);
//            return;
//        }
//        var timeUnitPath = propertyPath.Substring(0, lastIndexOf);
//        var timeUnit = property.serializedObject.FindProperty(timeUnitPath);
//        var unitTypeFromTimeUnit = property.serializedObject.FindProperty(timeUnitPath + ".unitType");
//        var indexOfUnitType = unitTypeFromTimeUnit.enumValueIndex;
//        var nameOfUnitType = unitTypeFromTimeUnit.enumNames[indexOfUnitType];
//        var baseName = "AdditionalInfo";
//        var reflectionName = nameOfUnitType + baseName;
//        var type = Type.GetType(reflectionName);
//        if (type == null){
//            Debug.LogError("No such reflection unit info " + reflectionName);
//            return;
//        }
//        var timelineUnitFromSerializedProperty = Tools.SerializedPropertyToObject<Skill.TimelineUnit>(timeUnit);
//        Skill.UnitAdditionalInfo unitAdditionalInfo = null;
//        if (timelineUnitFromSerializedProperty == null) return;
//        if (timelineUnitFromSerializedProperty.unitAdditionalInfo.GetType()!=type){
//            var constructors = type.GetConstructors();
//            unitAdditionalInfo = constructors[0].Invoke(null) as Skill.UnitAdditionalInfo;
//            timelineUnitFromSerializedProperty.unitAdditionalInfo = unitAdditionalInfo;
//            Debug.Log("创建了");
//        }
//        else{
//            unitAdditionalInfo = timelineUnitFromSerializedProperty.unitAdditionalInfo;
//            Debug.Log("读取了");
//        }
//        var fields = type.GetFields();
//        var startPosition = new Rect(position);
//        if (unitAdditionalInfo == null) return;
//        foreach (var field in fields){
//            var labelRect = new Rect(startPosition.x, startPosition.y, LabelWidth, 16);
//            var valueRect = new Rect(startPosition.x + 200, startPosition.y, LabelWidth, 16);
//            EditorGUI.LabelField(labelRect, field.Name);
//            if (field.FieldType == typeof(float)){
//                var res = field.GetValue(unitAdditionalInfo) is float ? (float) field.GetValue(unitAdditionalInfo) : -1;
//                res = EditorGUI.FloatField(valueRect, res);
//                field.SetValue(unitAdditionalInfo, res);
//            }
//            startPosition.y += 16;
//        }
//        property.serializedObject.ApplyModifiedProperties();
//        fieldNum = fields.Length;
//    }
//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
//        return fieldNum * base.GetPropertyHeight(property, label);
//    }
//}