using UnityEngine;

namespace SoulverTools.WorkData
{
    public static class ValueData
    {
        public static bool Range(int value, int? min, int? max) => value >= min && value <= max;

        public static bool Range(Vector2Int value, Vector2Int? min, Vector2Int? max) =>
            Range(value.x, min?.x, max?.x) && Range(value.y, min?.y, max?.y);
        
        public static bool RangeNotEqual(int value, int? min, int? max) => value > min && value < max;
        public static bool Range0NotEqual(int value, int? min, int? max) => value >= min && value < max;
        public static bool RangeNotEqual(Vector2Int value, Vector2Int? min, Vector2Int? max) =>
            RangeNotEqual(value.x, min?.x, max?.x) && RangeNotEqual(value.y, min?.y, max?.y);
        
        public static bool Range0NotEqual(Vector2Int value, Vector2Int? min, Vector2Int? max) =>
            Range0NotEqual(value.x, min?.x, max?.x) && Range0NotEqual(value.y, min?.y, max?.y);

        public static int Normalize(float value)
        {
            return value switch
            {
                0 => 0,
                < 0 => -1,
                _ => 1
            };
        }
    }
}