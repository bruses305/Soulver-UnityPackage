using System;
using UnityEngine;

namespace SoulverTools
{
    public class ShowIfAttribute : PropertyAttribute
    {
        public readonly string FieldName;

        public ShowIfAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
