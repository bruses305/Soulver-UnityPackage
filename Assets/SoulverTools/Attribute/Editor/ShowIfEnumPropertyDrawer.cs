using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SoulverTools
{
    [CustomPropertyDrawer(typeof(ShowIfEnumAttribute))]
    public class ShowIfEnumPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfEnumAttribute showIfEnumAttribute = (ShowIfEnumAttribute)attribute;
            SerializedProperty enumProperty = property.serializedObject.FindProperty(showIfEnumAttribute.EnumFieldName);
    
            if (Validate(enumProperty))
            {
                if (showIfEnumAttribute.EnumValues.Contains(enumProperty.enumValueIndex))
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
            else
            {
                EditorGUI.LabelField(position, $"ShowIfEnum: name {showIfEnumAttribute.EnumFieldName} not found or not Enum");
            }
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfEnumAttribute showIfEnumAttribute = (ShowIfEnumAttribute)attribute;
            SerializedProperty enumProperty = property.serializedObject.FindProperty(showIfEnumAttribute.EnumFieldName);
            if (Validate(enumProperty))
            {
                return  showIfEnumAttribute.EnumValues.Contains(enumProperty.enumValueIndex)
                    ? EditorGUI.GetPropertyHeight(enumProperty, label, true)
                    : 0f;
            }
            
            return EditorGUIUtility.singleLineHeight;
        }
    
        private static bool Validate(SerializedProperty enumProperty)
        {
            return enumProperty is { propertyType: SerializedPropertyType.Enum };
        }
    }
}
