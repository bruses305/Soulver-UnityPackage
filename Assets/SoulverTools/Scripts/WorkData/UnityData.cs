using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoulverTools.WorkData
{
    public static class UnityData
    {
        public static GameObject[] ChildOnMassive(Transform parent, int childCount)
        {
            GameObject[] children = new GameObject[childCount];
            int index = 0;
            foreach (Transform child in parent)
            {
                if (index == childCount) break;
                children[index] = child.gameObject;
                index++;
            }

            return children;
        }
        
        public static Image GetImage(Image[] components, GameObject parent, int idVisualSlot)
        {
            Image component;
            if (!components[idVisualSlot])
            {
                parent.TryGetComponent(out component);
                components[idVisualSlot] = component;
            }
            else
            {
                component = components[idVisualSlot];
            }
      
            return component;
        }

        public static void SetAlpha(TextMeshProUGUI TMP,float alpha) => TMP.color = new Color(TMP.color.r, TMP.color.g, TMP.color.b, alpha);
        public static void SetAlpha(Image image,float alpha) => image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        public static void SetAlpha(GameObject image, float alpha) => SetAlpha(image.GetComponent<Image>(), alpha);

        public static void AddFunctionButton(Button button, UnityAction callback)=>
            button.onClick.AddListener(callback);

        public static void AddFunctionButton(EventTrigger button, Action callback, EventTriggerType type = EventTriggerType.PointerClick)
        {
            EventTrigger.Entry eventTrigger = button.triggers.Find(entry => entry.eventID == type);
            bool triggerCreate = eventTrigger == null;
            eventTrigger ??= new();
            {
                eventTrigger.eventID = type; // event type (click, hold, etc.)
            }
            
            eventTrigger.callback.AddListener(_ => callback.Invoke());
            if(triggerCreate)
                button.triggers.Add(eventTrigger);
        }
        
        public static float GetAnimationLength(Animator animator, string animationName)
        {
            if(!animator) return 0f;
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            foreach (var clip in ac.animationClips)
            {
                if (clip.name == animationName)
                {
                    return clip.length;
                }
            }

            return 0f;
        }


    }
}
