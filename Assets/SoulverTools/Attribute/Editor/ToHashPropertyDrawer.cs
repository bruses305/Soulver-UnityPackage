using UnityEditor;
using UnityEngine;

namespace SoulverTools
{
    [CustomPropertyDrawer(typeof(ToHashAttribute))]
    public class StringToHashPropertyDrawer : PropertyDrawer
    {
        private const int mask = 0x3FFFFFFF;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ToHashAttribute toHashAttribute = (ToHashAttribute)attribute;
            SerializedProperty propertyValue = property.serializedObject.FindProperty(toHashAttribute.PropertyName); // Поиск строки
            property.intValue = Animator.StringToHash(propertyValue?.stringValue) & mask; // Переводим строку в Hash
            
            GUI.enabled = false; // Отключаем редактирование
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true; // Включаем обратно
        }
    }
}