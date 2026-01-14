using UnityEngine;

namespace SoulverTools
{
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Audio/VolumeAudioSource")]
    public class VolumeAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float baseVolume;

        private void Awake()
        {
            audioSource ??= GetComponent<AudioSource>();
            AudioSystem.OnVolumeChanged += OnVolumeChanged;
            OnVolumeChanged(AudioSystem.GetVolume());
        }

        private void OnVolumeChanged(float obj) =>
            audioSource.volume = obj * baseVolume * 1.00f;
    
        private void OnDestroy() =>
            AudioSystem.OnVolumeChanged -= OnVolumeChanged;
    
#if UNITY_EDITOR
        private void OnValidate()
        {
            audioSource ??= gameObject.GetComponent<AudioSource>();
        }
#endif
    }
}