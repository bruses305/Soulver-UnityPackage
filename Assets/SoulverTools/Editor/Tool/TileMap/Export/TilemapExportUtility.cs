using System;
using System.IO;
using SoulverTools.MenuTool.Data;
using SoulverTools.MenuTool.Method;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

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
                    return new ExportResult
                    {
                        Success = false,
                        Message = "No GameObject selected."
                    };
                }

                if (!selected.TryGetComponent<Tilemap>(out Tilemap tilemap))
                {
                    return new ExportResult
                    {
                        Success = false,
                        Message = "Selected object does not contain a Tilemap."
                    };
                }

                // ---------- FILE PATH ----------
                string fileName =
                    $"{tilemap.name}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

                string filePath = Path.Combine(
                    DefaultExportFolder,
                    fileName
                );

                // ---------- EXPORT CALL ----------
                bool result = TilemapExportReadTrue.ExportTilemapToPNG(
                    tilemap,
                    filePath,
                    pixelsPerUnit
                );

                return new ExportResult
                {
                    Success = result, Message = result ? "Tilemap exported successfully." : "Tilemap exported fail."
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
    }
}