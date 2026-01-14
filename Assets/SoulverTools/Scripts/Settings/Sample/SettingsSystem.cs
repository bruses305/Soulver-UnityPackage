using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SoulverTools.WorkData;

namespace SoulverTools
{
   public class SettingsSystem : MonoBehaviour
   {
      public static SettingsSystem Instance { get;private set; }
      [SerializeField] private GameObject settingsPanel;
      [SerializeField] private Scrollbar volumeScroller;
      [SerializeField] private TextMeshProUGUI volumeView;
      [SerializeField] private Toggle setShowAd;

      private Coroutine _viewVolume;

      public void SetActive(bool active) =>
         settingsPanel.SetActive(active);

      private void Awake()
      {
         settingsPanel.SetActive(false);
         Instance = this;
         volumeScroller.value = AudioSystem.GetVolume();
         UnityData.SetAlpha(volumeView, 0);
         volumeScroller.onValueChanged.AddListener(AudioSystem.SetVolume);
         volumeScroller.onValueChanged.AddListener(CustomViewPercentVolume);
      }


      private void CustomViewPercentVolume(float value)
      {
         UnityData.SetAlpha(volumeView, 1);
         volumeView.text = (value).ToString("P1");
         if (_viewVolume != null)
         {
            StopCoroutine(_viewVolume);
         }

         _viewVolume = StartCoroutine(ViewVolume());
      }

      private IEnumerator ViewVolume()
      {
         float timeHide = 2f;
         float time = 0;
         while(time<timeHide)
         {
            yield return new WaitForEndOfFrame();
            time = (1-volumeView.color.a) * timeHide;
            time += Time.deltaTime;
            UnityData.SetAlpha(volumeView, 1 - time / timeHide);
         }
      
         yield return new WaitForEndOfFrame();
         UnityData.SetAlpha(volumeView, 0);
         _viewVolume = null;
      }
   }
}
