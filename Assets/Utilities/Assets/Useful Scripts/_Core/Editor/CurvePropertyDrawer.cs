﻿using UnityEngine;
using UnityEditor;

namespace UsefulThings {
    [CustomPropertyDrawer(typeof(Curve))]
    public class CurvePropertyDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return property.isExpanded ? 52f : 16f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            label = EditorGUI.BeginProperty(position, label, property);

            position.height = 16;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("curve"), label);
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, new GUIContent(""), true);

            if (!property.isExpanded) {
                EditorGUI.EndProperty();
                return;
            }


            position = EditorGUI.IndentedRect(position);
            position.y += 18;
            position.width -= 20;
            position.width /= 2;
            int oldIndentLevel = EditorGUI.indentLevel;

            EditorGUI.indentLevel = 0;
            EditorGUIUtility.labelWidth = 32f;

            float startX = position.x;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("amplitude"), new GUIContent("AMP"));
            position.x += position.width + 10;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("frequency"), new GUIContent("FRQ"));

            position.x = startX;
            position.y += 18;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("offset"), new GUIContent("OFF"));
            position.x += position.width + 10;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("phase"), new GUIContent("PHS"));

            EditorGUI.EndProperty();

            EditorGUI.indentLevel = oldIndentLevel;
        }
    }
}