using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

namespace SoulverTools.MenuTool
{
    public static class ExportSelectedPackage
    {
        [MenuItem("Assets/Export Selected Package+", true)]
        private static bool ValidateExportPackage()
        {
            // Available only if an object is selected from the "Packages/" folder
            if (Selection.assetGUIDs.Length == 0) return false;
            var path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            return path.StartsWith("Packages/");
        }

        [MenuItem("Assets/Export Selected Package+")]
        private static void ExportSelected()
        {
            // Getting the path to the selected package
            string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            string packageRoot = GetPackageRoot(path);

            if (string.IsNullOrEmpty(packageRoot) || !Directory.Exists(packageRoot))
            {
                EditorUtility.DisplayDialog("Error", "Select the package folder from' Packages/!", "OK");
                return;
            }

            // The name of the file to export
            string exportFile = $"{Path.GetFileName(packageRoot)}.unitypackage";
            string savePath = EditorUtility.SaveFilePanel("Save Unity Package", "", exportFile, "unitypackage");
            if (string.IsNullOrEmpty(savePath)) return;

            // We find all the package files
            var files = Directory.GetFiles(packageRoot, "*.*", SearchOption.AllDirectories)
                .Where(f => !f.EndsWith(".meta"))
                .Select(f => f.Replace("\\", "/"))
                .ToArray();

            if (files.Length == 0)
            {
                EditorUtility.DisplayDialog("Empty", "There are no files to export in the package.", "OK");
                return;
            }

            AssetDatabase.ExportPackage(files, savePath, ExportPackageOptions.Interactive);
            EditorUtility.DisplayDialog("Done", $"✅ Exported {files.Length} files from {packageRoot}", "OK");
            Debug.Log($"✅ Exported package: {packageRoot} → {savePath}");
        }

        private static string GetPackageRoot(string path)
        {
            // Example: Packages/com.company.MyTool/Runtime/script.cs → Packages/com.company.MyTool
            string[] parts = path.Split('/');
            if (parts.Length < 2) return null;
            return Path.Combine(parts[0], parts[1]).Replace("\\", "/");
        }
    }
}
