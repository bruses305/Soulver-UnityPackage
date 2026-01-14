using System;
using UnityEngine;

namespace SoulverTools
{
    public static class TranslationSystem
    {
        public enum Language
        {
            Russian,
            English,
        }

        public static event Action OnLanguageChanged;
        private static Language TotalLanguage { get; set; }

        public static void SetLanguage(Language language)
        {
            if(language != TotalLanguage)
                OnLanguageChanged?.Invoke();
            TotalLanguage = language;
        }
    

        public static Sprite GetSprite(string key)
        {
            return null;
        }

        public static string GetString(string key)
        {
            return null;
        }
    
    
    }
}
