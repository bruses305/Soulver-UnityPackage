using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SoulverTools.MenuTool
{
    public class TilemapExportWindow : EditorWindow
    {
        private const string MenuPath = "SoulverTools/Exporter/Selected Tilemap To PNG";
        private static int pixelsPerUnit = 100;

        [MenuItem(MenuPath)]
        private static void TilemapExport()
        {
            GetWindow<TilemapExportWindow>("Tilemap Export");
        }

        [MenuItem(MenuPath, true)]
        private static bool TilemapExport_Validate()
        {
            return Selection.activeGameObject != null &&
                   Selection.activeGameObject.TryGetComponent<Tilemap>(out _);
        }
        
        private void OnEnable()
        {
            minSize = new Vector2(400, 100);
            maxSize = new Vector2(400, 100);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Export Settings", EditorStyles.boldLabel);

            pixelsPerUnit = EditorGUILayout.IntField("Pixels Per Unit", pixelsPerUnit);
            pixelsPerUnit = Mathf.Clamp(pixelsPerUnit, 1, 4096);

            GUILayout.Space(10);

            using (new EditorGUI.DisabledScope(!Selection.activeGameObject))
            {
                if (GUILayout.Button("Export Selected Tilemap"))
                {
                    var result = TilemapExportUtility.ExportSelectedTilemap(pixelsPerUnit);
                    
                    EditorUtility.DisplayDialog(
                        "Tilemap Export" + (result.Success ? "" : "Error"),
                        result.Message,
                        result.Success ? "OK" : "Cancel"
                    );
                    
                    Close();
                }
            }
        }
    }
}