using UnityEditor;
using UnityEngine;

namespace _Project.GridWindow
{
    public static class LevelEditorExtension
    {
        public static void SerializedCustomPropetry(Object @object, string propertyName)
        {
            if (@object == null) return;

            SerializedObject serializedObject = new SerializedObject(@object);
            SerializedProperty containerProperty = serializedObject.FindProperty(propertyName);
            EditorGUILayout.PropertyField(containerProperty, true);
            serializedObject.ApplyModifiedProperties();
        }

        public static void BaseMidlleText(string text, int fontSize, int space = 0)
        {
            EditorGUILayout.Space(space);
            GUIStyle centeredStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = fontSize
            };
            EditorGUILayout.LabelField(text, centeredStyle, GUILayout.ExpandWidth(true));
        }
    }
}