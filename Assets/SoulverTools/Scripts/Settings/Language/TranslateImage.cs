using UnityEngine;
using UnityEngine.UI;

namespace SoulverTools
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("Translate/Translate Image")]
    public class TranslateImage : MonoBehaviour
    {
        [SerializeField] private Image spriteRenderer;
        [SerializeField] private string key;

        private void Start()
        {
            // стартовая загрузка текста
            spriteRenderer ??= GetComponent<Image>();
            OnLanguageChanged();
        }

        private void OnEnable() =>
            TranslationSystem.OnLanguageChanged += OnLanguageChanged;

        private void OnDisable() =>
            TranslationSystem.OnLanguageChanged -= OnLanguageChanged;

        private void OnLanguageChanged()
        {
            Sprite sprite = TranslationSystem.GetSprite(key);
        
            if (sprite != null)
                spriteRenderer.sprite = TranslationSystem.GetSprite(key);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            spriteRenderer = gameObject.GetComponent<Image>();
        }
#endif
    }
}