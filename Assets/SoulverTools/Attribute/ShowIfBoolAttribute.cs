using System;
using UnityEngine;

namespace SoulverTools
{
    public class ShowIfBoolAttribute : PropertyAttribute
    {
        public readonly string FieldName;
        public readonly bool Value;

        public ShowIfBoolAttribute(string fieldName, bool value = true)
        {
            FieldName = fieldName;
            Value = value;
        }
    }
}
