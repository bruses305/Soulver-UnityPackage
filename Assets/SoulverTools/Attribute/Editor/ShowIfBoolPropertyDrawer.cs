using UnityEditor;
using UnityEngine;

namespace SoulverTools
{
    [CustomPropertyDrawer(typeof(ShowIfBoolAttribute))]
    public class ShowIfBoolPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfBoolAttribute showIfBoolAttribute = (ShowIfBoolAttribute)attribute;
            SerializedProperty propertyValue = property.serializedObject.FindProperty(showIfBoolAttribute.FieldName);
            if (propertyValue.boolValue == showIfBoolAttribute.Value)
                EditorGUI.PropertyField(position, property, label, true);
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfBoolAttribute showIfBoolAttribute = (ShowIfBoolAttribute)attribute;
            SerializedProperty propertyValue = property.serializedObject.FindProperty(showIfBoolAttribute.FieldName);
            
            return  propertyValue?.boolValue == showIfBoolAttribute.Value
                ? EditorGUI.GetPropertyHeight(property)
                : 0f;
        }
    }
}
