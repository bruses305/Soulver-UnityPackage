using System;

namespace SoulverTools
{
    public static class AudioSystem
    {
        public static event Action<float> OnVolumeChanged;

        private static float _volume = MemoryGameData.LoadVolume();

        public static void SetVolume(float volume)
        {
            _volume = volume;
            MemoryGameData.SaveVolume(_volume);
            OnVolumeChanged?.Invoke(volume);
        }

        public static float GetVolume() => _volume;
    }
}