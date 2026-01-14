using UnityEngine;
using UnityEngine.UI;
using SoulverTools.WorkData;

namespace SoulverTools
{
    public class PolicyConfidentialityObject : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button cancelButton;
        private Color _toggleColor;

        private void Awake()
        {
            _toggleColor = toggle.targetGraphic.color;
            toggle.onValueChanged.AddListener(SetToggled);
            UnityData.AddFunctionButton(acceptButton, Accept);
            UnityData.AddFunctionButton(cancelButton, Application.Quit);
        }

        private void Accept()
        {
            if (toggle.isOn)
            {
                PolicyActivator.SetConsentState();
                MemoryGameData.SavePolicyState(true);
                Destroy(gameObject);
            }
            else
            {
                toggle.targetGraphic.color = Color.crimson;
            }
        }

        private void SetToggled(bool toggled) =>
            toggle.targetGraphic.color = _toggleColor;
    
        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }
    }
}