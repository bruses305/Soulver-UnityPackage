using UnityEngine;

namespace SoulverTools
{
    public static partial class MemoryGameData
    {
        private const string PolicyConfirmed = "PolicyConfirmed";

        public static void SavePolicyState(bool policyConfirmed)
        {
            PlayerPrefs.SetInt(PolicyConfirmed, policyConfirmed ? 1 : 0);
        }
        
        public static bool GetPolicyState()
        {
            return PlayerPrefs.GetInt(PolicyConfirmed, 0) == 1;
        }
        
    }
}