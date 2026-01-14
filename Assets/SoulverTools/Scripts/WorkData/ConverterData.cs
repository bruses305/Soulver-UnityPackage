using UnityEngine;

namespace SoulverTools.WorkData.Converter
{
    public static class ConverterData
    {
        public static Vector3Int GetWorldPosTile(Vector3 worldPos) {
            int xInt = Mathf.FloorToInt(worldPos.x);
            int yInt = Mathf.FloorToInt(worldPos.y);
            return new(xInt, yInt, 0);
        }
    }
}
