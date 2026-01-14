using UnityEngine;

namespace SoulverTools
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Translate/Translate SpriteRenderer")]
    public class TranslateSpriteRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private string key;

        private void Start()
        {
            // стартовая загрузка текста
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = TranslationSystem.GetSprite(key);
        }

        private void OnEnable() =>
            TranslationSystem.OnLanguageChanged += OnLanguageChanged;

        private void OnDisable() =>
            TranslationSystem.OnLanguageChanged -= OnLanguageChanged;

        private void OnLanguageChanged()=>
            spriteRenderer.sprite = TranslationSystem.GetSprite(key);

#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
#endif
    }
}