using UnityEngine;

namespace SoulverTools
{
    public class ShowIfEnumAttribute : PropertyAttribute
    {
        public readonly string EnumFieldName;
        public readonly int[] EnumValues;

        public ShowIfEnumAttribute(string enumFieldName, int[] enumValues = null)
        {
            EnumFieldName = enumFieldName;
            EnumValues = enumValues;
        }

        public ShowIfEnumAttribute(string enumFieldName, int enumValue) : 
            this(enumFieldName, new int[] { enumValue }){ }
        
    }
}
