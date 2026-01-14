using UnityEngine;

namespace SoulverTools
{
    public static partial class MemoryGameData
    {
        private const string SkinPrefs = "skinContainer/";
        
#if !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void UpdateLoadOldData()
        {
            LoadLevelData();
            LoadSkinData();
        }
#endif

        public static void SaveVolume(float volume) =>
            PlayerPrefs.SetFloat("Volume", volume);

        public static float LoadVolume() =>
            PlayerPrefs.GetFloat("Volume", 0.5f);

    }
}