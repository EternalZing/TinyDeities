using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Tools{
    // Start is called before the first frame update
    public static T SerializedPropertyToObject<T>(SerializedProperty property){
        return GetNestedObject<T>(property.propertyPath, GetSerializedPropertyRootComponent(property),
            true); //The "true" means we will also check all base classes
    }

    public static Object GetSerializedPropertyRootComponent(SerializedProperty property){
        return property.serializedObject.targetObject;
    }

    public static T GetNestedObject<T>(string path, object obj, bool includeAllBases = false){
        foreach (var part in path.Split('.')){
            obj = GetFieldOrPropertyValue<object>(part, obj, includeAllBases);
            Debug.Log(obj);
            if (obj == null) continue;

            if (obj.GetType().GetGenericTypeDefinition() == typeof(List<>)){
                var first = path.LastIndexOf('[');
                var last = path.LastIndexOf(']');
                var index = int.Parse(path.Substring(first + 1, last - first - 1));
                var list = (List<T>) obj;
                return list[index];
            }
        }

        return (T) obj;
    }

    public static T GetFieldOrPropertyValue<T>(string fieldName, object obj, bool includeAllBases = false,
        BindingFlags bindings =
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic){
        var field = obj.GetType().GetField(fieldName, bindings);
        if (field != null) return (T) field.GetValue(obj);
        var property = obj.GetType().GetProperty(fieldName, bindings);
        if (property != null) return (T) property.GetValue(obj, null);

        if (includeAllBases)
            foreach (var type in GetBaseClassesAndInterfaces(obj.GetType())){
                field = type.GetField(fieldName, bindings);
                if (field != null) return (T) field.GetValue(obj);

                property = type.GetProperty(fieldName, bindings);
                if (property != null) return (T) property.GetValue(obj, null);
            }

        return default;
    }

    public static void SetFieldOrPropertyValue<T>(string fieldName, object obj, object value,
        bool includeAllBases = false,
        BindingFlags bindings =
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic){
        var field = obj.GetType().GetField(fieldName, bindings);
        if (field != null){
            field.SetValue(obj, value);
            return;
        }

        var property = obj.GetType().GetProperty(fieldName, bindings);
        if (property != null){
            property.SetValue(obj, value, null);
            return;
        }

        if (includeAllBases)
            foreach (var type in GetBaseClassesAndInterfaces(obj.GetType())){
                field = type.GetField(fieldName, bindings);
                if (field != null){
                    field.SetValue(obj, value);
                    return;
                }

                property = type.GetProperty(fieldName, bindings);
                if (property != null){
                    property.SetValue(obj, value, null);
                    return;
                }
            }
    }

    public static IEnumerable<Type> GetBaseClassesAndInterfaces(this Type type, bool includeSelf = false){
        var allTypes = new List<Type>();

        if (includeSelf) allTypes.Add(type);

        if (type.BaseType == typeof(object))
            allTypes.AddRange(type.GetInterfaces());
        else
            allTypes.AddRange(
                Enumerable
                    .Repeat(type.BaseType, 1)
                    .Concat(type.GetInterfaces())
                    .Concat(type.BaseType.GetBaseClassesAndInterfaces())
                    .Distinct());
        //I found this on stackoverflow

        return allTypes;
    }
}