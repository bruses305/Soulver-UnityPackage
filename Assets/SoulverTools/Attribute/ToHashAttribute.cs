using UnityEngine;

namespace SoulverTools
{
    public class ToHashAttribute : PropertyAttribute
    {
        public readonly string PropertyName;

        public ToHashAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}