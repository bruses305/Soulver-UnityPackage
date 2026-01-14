using UnityEngine;
using UnityEngine.EventSystems;
using UnityData = SoulverTools.WorkData.UnityData;

namespace SoulverTools
{
    [RequireComponent(typeof(EventTrigger))]
    public class ViewContent : MonoBehaviour
    {
        [SerializeField] private GameObject content;

        private EventTrigger _eventTrigger;

        private void Awake()
        {
            _eventTrigger = GetComponent<EventTrigger>();
            DeselectObject();

            UnityData.AddFunctionButton(_eventTrigger, SelectObject, EventTriggerType.PointerEnter);
            UnityData.AddFunctionButton(_eventTrigger, DeselectObject, EventTriggerType.PointerExit);
        }

        private void DeselectObject() =>
            content.SetActive(false);

        private void SelectObject() =>
            content.SetActive(true);
    }
}
