using UnityEditor;
using UnityEngine;

namespace StuntGame
{
    [CustomEditor(typeof(Vehicle))]
    public class CharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            var obj = target as Vehicle;

            obj.wheelRadius = EditorGUILayout.FloatField("Wheel Radius", obj.wheelRadius);
            obj.wheelModel = (GameObject)EditorGUILayout.ObjectField(obj.wheelModel, typeof(GameObject), true);

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("wheels"), true);
            serializedObject.ApplyModifiedProperties();

            obj.wheels[0].name = "Front Left";
            obj.wheels[1].name = "Front Right";
            obj.wheels[2].name = "Back Left";
            obj.wheels[3].name = "Back Right";

            if(EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}