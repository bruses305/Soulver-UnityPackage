using UnityEditor;
using UnityEngine;

namespace SoulverTools
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Всегда начинаем с BeginProperty
            EditorGUI.BeginProperty(position, label, property);

            try
            {
                // Проверка на null (бывает, если тип не сериализуемый)
                if (property == null)
                {
                    EditorGUI.LabelField(position, label.text, "Property is null");
                }
                else if (!property.hasVisibleChildren)
                {
                    // Отрисуем без рекурсии
                    GUI.enabled = false;
                    EditorGUI.PropertyField(position, property, label, false);
                    GUI.enabled = true;
                }
                else
                {
                    // Если есть вложенные поля, отрисуем их
                    GUI.enabled = false;
                    EditorGUI.PropertyField(position, property, label, true);
                    GUI.enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                // Чтобы инспектор не падал окончательно
                Debug.LogError($"[CustomDrawer] Ошибка при отрисовке {property?.name}: {ex}");
                EditorGUI.LabelField(position, label.text, "Draw Error");
            }

            // Завершаем всегда
            EditorGUI.EndProperty();
        }
    }
}
