using System.Collections.Generic; 
using System.Linq;
using UnityEngine; 
using UnityEngine.U2D.Animation; 
using SoulverTools.WorkData;

namespace SoulverTools
{
    [ExecuteAlways]
    [RequireComponent(typeof(SpriteResolver))]
    public class SpriteResolverAnimation : MonoBehaviour
    {
        [SerializeField] private bool categoryStatic;

        [ShowIfBool(nameof(categoryStatic), false), SerializeField, Min(0)]
        private int categoryID;

        [SerializeField, Min(0)] private int labelID;
        [ReadOnly, SerializeField] private SpriteResolver spriteResolver;
#if UNITY_EDITOR
        [SerializeField] private bool preview;

        [ReadOnly, SerializeField] private string totalCategory;
        [ReadOnly, SerializeField] private string totalLabel;
#endif
        [SerializeField] private List<string> categoryNames;
        [ReadOnly, SerializeField] private List<string> labelNames;

#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteResolver ??= GetComponent<SpriteResolver>();
            SpriteLibraryAsset spriteLibraryAsset = spriteResolver.spriteLibrary.spriteLibraryAsset;
            if (spriteLibraryAsset)
                return;
            categoryNames = spriteLibraryAsset.GetCategoryNames().ToList();

            if (!ValueData.Range0NotEqual(categoryID, 0, categoryNames.Count)) return;

            totalCategory = categoryStatic ? spriteResolver.GetCategory() : categoryNames[categoryID];
            labelNames = spriteLibraryAsset.GetCategoryLabelNames(totalCategory).ToList();

            if (!ValueData.Range0NotEqual(labelID, 0, labelNames.Count)) return;

            totalLabel = labelNames[labelID];
            if (preview) UpdateVisual();
        }
#endif
        private void Awake()
        {
            if (!Application.isPlaying) return;
            spriteResolver ??= GetComponent<SpriteResolver>();
            SpriteLibraryAsset spriteLibraryAsset = spriteResolver.spriteLibrary.spriteLibraryAsset;
            categoryNames ??= spriteLibraryAsset.GetCategoryNames().ToList();

            string categoryName = categoryStatic ? spriteResolver.GetCategory() : categoryNames[categoryID];
        }

        private void OnDidApplyAnimationProperties() => UpdateVisual();

        private void UpdateVisual()
        {
            string categoryName = categoryStatic ? spriteResolver.GetCategory() : categoryNames[categoryID];
            if (categoryStatic == false)
                labelNames = spriteResolver.spriteLibrary.spriteLibraryAsset.GetCategoryLabelNames(categoryName)
                    .ToList();
            spriteResolver.SetCategoryAndLabel(categoryName, labelNames[labelID]);
        }
    }
}