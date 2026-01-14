#if UNITY_EDITOR
using System;
using UnityEngine;

namespace SoulverTools.Grid
{
    [Serializable]
    public struct GridData
    {
        [SerializeField] public Vector2Int position;
        [SerializeField] public Vector2Int size;
        [SerializeField] public Color color;
    }
}
#endif