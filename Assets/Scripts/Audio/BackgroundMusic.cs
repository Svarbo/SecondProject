using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private GameAudioData _gameAudioData;

        private AudioSource _audioSource;

        private void Awake() =>
            _audioSource = GetComponent<AudioSource>();

        private void OnEnable()
        {
            ChangeVolume(_gameAudioData.MusicVolume);
            _gameAudioData.MusicCVolumeChanged += ChangeVolume;
        }

        private void OnDisable() =>
            _gameAudioData.MusicCVolumeChanged -= ChangeVolume;

        public void ChangeVolume(float value)
        {
            float volume = Mathf.Clamp01(value);
            _audioSource.volume = volume;
        }
    }
}