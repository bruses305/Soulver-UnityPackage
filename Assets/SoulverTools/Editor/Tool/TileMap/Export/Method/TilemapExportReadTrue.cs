using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SoulverTools.MenuTool.Method
{
    public static class TilemapExportReadTrue
    {
        // session-only flag
        public static bool SkipNonReadableWarning = false;

        // ReSharper disable Unity.PerformanceAnalysis
        public static bool ExportTilemapToPNG(
            Tilemap tilemap,
            string filePath,
            int pixelsPerUnit = 100
        )
        {
            if (tilemap == null)
            {
                Debug.LogError("Tilemap is null");
                return false;
            }

            // ---------- READABLE CHECK ----------
            if (!IsTilemapReadable(tilemap))
            {
                return HandleNonReadable(tilemap, filePath, pixelsPerUnit);
            }

            // ---------- BOUNDS ----------
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            Sprite referenceSprite = null;

            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                Sprite sprite = tilemap.GetSprite(pos);
                if (sprite == null)
                    continue;

                referenceSprite = sprite;

                minX = Mathf.Min(minX, pos.x);
                minY = Mathf.Min(minY, pos.y);
                maxX = Mathf.Max(maxX, pos.x);
                maxY = Mathf.Max(maxY, pos.y);
            }

            if (referenceSprite == null)
            {
                Debug.LogWarning("Tilemap contains no sprites");
                return false;
            }

            int tileWidth = (int)referenceSprite.rect.width;
            int tileHeight = (int)referenceSprite.rect.height;

            int width = (maxX - minX + 1) * tileWidth;
            int height = (maxY - minY + 1) * tileHeight;

            // ---------- TEXTURE ----------
            Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false);
            Color[] clear = new Color[width * height];
            result.SetPixels(clear);

            // ---------- PIXEL COPY ----------
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                Sprite sprite = tilemap.GetSprite(pos);
                if (sprite == null)
                    continue;

                Texture2D spriteTex = ExtractSpriteTexture(sprite);

                int px = (pos.x - minX) * tileWidth;
                int py = (pos.y - minY) * tileHeight;

                result.SetPixels(px, py, tileWidth, tileHeight, spriteTex.GetPixels());
            }

            result.Apply();

            SaveData.SaveAsPNG(result, filePath);

            Object.DestroyImmediate(result);
            return true;
        }

        // ============================================================
        // ===================== HELPERS ==============================
        // ============================================================

        private static bool IsTilemapReadable(Tilemap tilemap)
        {
            foreach (var pos in tilemap.cellBounds.allPositionsWithin)
            {
                Sprite sprite = tilemap.GetSprite(pos);
                if (sprite == null)
                    continue;

                if (!sprite.texture.isReadable)
                    return false;
            }

            return true;
        }

        private static Texture2D ExtractSpriteTexture(Sprite sprite)
        {
            Texture2D source = sprite.texture;

            Color[] pixels = source.GetPixels(
                (int)sprite.textureRect.x,
                (int)sprite.textureRect.y,
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height
            );

            Texture2D tex = new Texture2D(
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height,
                TextureFormat.RGBA32,
                false
            );

            tex.SetPixels(pixels);
            tex.Apply();
            return tex;
        }

        // ============================================================
        // ================= NON-READABLE HANDLING ====================
        // ============================================================

        private static bool HandleNonReadable(
            Tilemap tilemap,
            string filePath,
            int pixelsPerUnit
        )
        {
            if (SkipNonReadableWarning)
            {
                TilemapExportReadFalse.ExportTilemapToPNG(
                    tilemap,
                    filePath,
                    pixelsPerUnit
                );
                return true;
            }

            int choice = EditorUtility.DisplayDialogComplex(
                "Sprite Read/Write Disabled",
                "One or more sprites used in this Tilemap have Read/Write disabled.\n\n" +
                "This export method requires readable textures.\n\n" +
                "You can use an alternative export method that creates a temporary scene.",
                "Use Alternative Export",
                "Cancel",
                null
            );

            if (choice != 0)
                return false;

            bool dontAskAgain = EditorUtility.DisplayDialog(
                "Don't ask again",
                "Skip this warning for the rest of this Unity session?",
                "Yes",
                "No"
            );

            if (dontAskAgain)
                SkipNonReadableWarning = true;

            TilemapExportReadFalse.ExportTilemapToPNG(
                tilemap,
                filePath,
                pixelsPerUnit
            );
            
            return true;
        }
    }
}