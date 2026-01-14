using UnityEditor;
using UnityEngine;

namespace SoulverTools.MenuTool
{
    public class ClearDataTool
    {
        private const string MenuPath = "SoulverTools/Clear All Data";

        [MenuItem(MenuPath)]
        public static void SetAllData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}