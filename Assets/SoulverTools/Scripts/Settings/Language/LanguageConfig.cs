using UnityEngine;

namespace SoulverTools
{
    [CreateAssetMenu(fileName = "LanguageConfig", menuName = "Config/LanguageConfig")]
    public class LanguageConfig : ScriptableObject
    {
#if UNITY_EDITOR

        private void OnValidate()
        {
        
        }

#endif
    }
}