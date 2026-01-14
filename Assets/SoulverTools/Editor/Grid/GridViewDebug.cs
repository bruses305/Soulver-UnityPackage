#if UNITY_EDITOR
using SoulverTools.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace SoulverTools
{
    public class GridViewDebug : MonoBehaviour
    {
        [SerializeField] private List<GridData> gridDataList;
        [SerializeField] private bool hide;

        private void OnDrawGizmos()
        {
            if (hide || gridDataList is not { Count: 0 }) return;
            foreach (var gridData in gridDataList)
            {
                DrawGrid(gridData);
            }
        }

        private void DrawGrid(GridData gridData)
        {
            Gizmos.color = gridData.color;
            DrawGrid(gridData.position, gridData.size);
        }

        private void DrawGrid(Vector2Int position, Vector2Int size)
        {
            float left = size.x / 2.0f;
            float top = size.y / 2.0f;

            for (int x = 0; x <= size.x; x++)
            {
                Gizmos.DrawLine(new Vector2(x + position.x, 0 + position.y),
                    new Vector2(x + position.x, size.y + position.y));
            }

            for (int y = 0; y <= size.y; y++)
            {
                Gizmos.DrawLine(new Vector2(0 + position.x, y + position.y),
                    new Vector2(size.x + position.x, y + position.y));
            }
        }
    }
}
#endif
