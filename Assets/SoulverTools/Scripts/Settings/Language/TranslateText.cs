using TMPro;
using UnityEngine;

namespace SoulverTools
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [AddComponentMenu("Translate/Translate Text")]
    public class TranslateText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TMP;
        private string key;

        private void Awake()
        {
            // стартовая загрузка текста
            TMP ??= GetComponent<TextMeshProUGUI>();
            TMP.text = TranslationSystem.GetString(key);
        }

        private void OnEnable() =>
            TranslationSystem.OnLanguageChanged += OnLanguageChanged;

        private void OnDisable() =>
            TranslationSystem.OnLanguageChanged -= OnLanguageChanged;

        private void OnLanguageChanged()=>
            TMP.text = TranslationSystem.GetString(key);

#if UNITY_EDITOR
        private void OnValidate()
        {
            TMP = gameObject.GetComponent<TextMeshProUGUI>();
        }
#endif
    }
}