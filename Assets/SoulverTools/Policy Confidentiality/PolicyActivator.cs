using UnityEngine;
using UnityEngine.UnityConsent;

namespace SoulverTools
{
    public class PolicyActivator : MonoBehaviour
    {
        public static bool PolicyConfirmed;
        private const string PolicyConfirmedPrefab = "PolicyConfirmed";

        private void Awake()
        {
            PolicyConfirmed = MemoryGameData.GetPolicyState();

            if (PolicyConfirmed)
            {
                SetConsentState();
                Destroy(gameObject);
            }
            else
            {
                Instantiate(Resources.Load<GameObject>(PolicyConfirmedPrefab));
                Destroy(gameObject);
            }
        }

        public static void SetConsentState()
        {
            PolicyConfirmed = true;
            EndUserConsent.SetConsentState(new ConsentState {
                AnalyticsIntent = ConsentStatus.Granted,
            });
        }
    }
}