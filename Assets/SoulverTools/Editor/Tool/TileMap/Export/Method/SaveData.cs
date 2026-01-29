using System.IO;
using UnityEditor;
using UnityEngine;

namespace SoulverTools.MenuTool.Method
{
    public static class SaveData
    {
        public static void SaveAsPNG(Texture2D texture, string filePath)
        {
            byte[] bytes = texture.EncodeToPNG();

            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllBytes(filePath, bytes);
            AssetDatabase.Refresh();
        }
    }
}