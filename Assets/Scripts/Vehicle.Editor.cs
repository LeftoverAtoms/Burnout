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

            obj.spring.Range = EditorGUILayout.Vector2Field("Spring Range", obj.spring.Range);
            obj.spring.damping = EditorGUILayout.FloatField("Wheel Radius", obj.wheelRadius);
            obj.spring.damping = EditorGUILayout.FloatField("Damper", obj.spring.damping);
            obj.spring.stiffness = EditorGUILayout.FloatField("Stiffness", obj.spring.stiffness);
            obj.spring.restLength = EditorGUILayout.FloatField("Rest Length", obj.spring.restLength);

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