using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEditor.SceneManagement;
using Object = UnityEngine.Object;

namespace SoulverTools.MenuTool
{
    public static class TilemapExportUtility
    {
        private const string DefaultExportFolder = "Assets/Export File/Editor";

        public static ExportResult ExportSelectedTilemap(int pixelsPerUnit = 100)
        {
            try
            {
                GameObject selected = Selection.activeGameObject;
                if (selected == null)
                {
                    Debug.LogWarning("No GameObject selected.");
                    return new ExportResult
                    {
                        Success = false,
                        Message = "No GameObject selected."
                    };
                }

                if (!selected.TryGetComponent<Tilemap>(out Tilemap tilemap))
                {
                    Debug.LogWarning("Selected object does not contain a Tilemap component.");
                    return new ExportResult
                    {
                        Success = false,
                        Message = "Selected object does not contain Tilemap."
                    };
                }

                string fileName = $"{tilemap.name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
                string filePath = Path.Combine(DefaultExportFolder, fileName);

                ExportTilemapToPNG(tilemap, filePath, pixelsPerUnit);
                return new ExportResult
                {
                    Success = true,
                    Message = "Tilemap exported successfully."
                };
            }
            catch (Exception e)
            {
                return new ExportResult
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        /// <summary>
        /// Основной метод экспорта Tilemap в PNG через временную сцену
        /// </summary>
        private static void ExportTilemapToPNG(Tilemap originalTilemap, string assetPath, int pixelsPerUnit = 100)
        {
            if (originalTilemap == null)
            {
                Debug.LogError("Tilemap is null.");
                return;
            }

            // 1. Сохраняем все тайлы в Dictionary
            Dictionary<Vector3Int, TileBase> tiles = new Dictionary<Vector3Int, TileBase>();
            foreach (Vector3Int pos in originalTilemap.cellBounds.allPositionsWithin)
            {
                TileBase tile = originalTilemap.GetTile(pos);
                if (tile != null)
                {
                    tiles[pos] = tile;
                }
            }

            if (tiles.Count == 0)
            {
                Debug.LogWarning("Tilemap is empty, nothing to export.");
                return;
            }
            
            // 2.1 Сохраняем текущую сцену
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            string originalScene = EditorSceneManager.GetActiveScene().path;
            // 2.2 Создаём временную сцену
            var tempScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // 3. Создаём временный Grid + Tilemap
            GameObject gridGO = new GameObject("TempGrid");
            gridGO.AddComponent<UnityEngine.Grid>();
            
            GameObject tilemapGO = new GameObject("TempTilemap");
            tilemapGO.transform.SetParent(gridGO.transform);
            Tilemap tempTilemap = tilemapGO.AddComponent<Tilemap>();
            tilemapGO.AddComponent<TilemapRenderer>();

            // 4. Восстанавливаем тайлы
            foreach (var kvp in tiles)
            {
                tempTilemap.SetTile(kvp.Key, kvp.Value);
            }
            tempTilemap.RefreshAllTiles();

            // 5. Рассчитываем bounds
            BoundsInt boundsInt = tempTilemap.cellBounds;
            Bounds bounds = new Bounds(boundsInt.center, boundsInt.size);

            // 6. Создаём камеру для рендера
            GameObject camGO = new GameObject("TempCamera");
            Camera cam = camGO.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = bounds.size.y / 2f;
            cam.aspect = bounds.size.x / bounds.size.y;
            cam.clearFlags = CameraClearFlags.Color;
            cam.backgroundColor = Color.clear;
            cam.transform.position = gridGO.transform.TransformPoint(bounds.center) + Vector3.back * 10f;
            cam.cullingMask = 1 << tempTilemap.gameObject.layer;

            // 7. RenderTexture
            int width = Mathf.CeilToInt(bounds.size.x * pixelsPerUnit);
            int height = Mathf.CeilToInt(bounds.size.y * pixelsPerUnit);
            RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
            rt.filterMode = FilterMode.Point;
            cam.targetTexture = rt;

            // 8. Рендерим и читаем пиксели
            cam.Render();
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = rt;
            Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();
            RenderTexture.active = previous;

            // 9. Сохраняем PNG
            SaveTextureAsPNG(tex, assetPath);

            // 10. Чистим временные объекты
            Object.DestroyImmediate(tex);
            Object.DestroyImmediate(camGO);
            Object.DestroyImmediate(gridGO);
            rt.Release();
            Object.DestroyImmediate(rt);

            // 11. Закрываем временную сцену
            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            AssetDatabase.Refresh();
            
            // 12. Открываем старую сцену
            EditorSceneManager.OpenScene(originalScene);
        }

        private static void SaveTextureAsPNG(Texture2D texture, string assetPath)
        {
            try
            {
                string dir = Path.GetDirectoryName(assetPath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllBytes(assetPath, texture.EncodeToPNG());
                Debug.Log($"Tilemap exported to: {assetPath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save PNG: {e.Message}");
            }
        }
    }
}
