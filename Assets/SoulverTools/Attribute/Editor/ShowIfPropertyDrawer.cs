using UnityEditor;
using UnityEngine;

namespace SoulverTools
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIfAttribute = (ShowIfAttribute)attribute;
            SerializedProperty propertyValue = property.serializedObject.FindProperty(showIfAttribute.FieldName);
            if (propertyValue?.objectReferenceValue)
                EditorGUI.PropertyField(position, property, label, true);
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIfAttribute = (ShowIfAttribute)attribute;
            SerializedProperty propertyValue = property.serializedObject.FindProperty(showIfAttribute.FieldName);
            
            return  propertyValue?.objectReferenceValue
                ? EditorGUI.GetPropertyHeight(property)
                : 0f;
        }
    }
}
