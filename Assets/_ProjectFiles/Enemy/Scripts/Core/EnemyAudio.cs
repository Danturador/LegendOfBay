using _ProjectFiles.SoundContainer;
using UnityEngine;
using Zenject;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyAudio : MonoBehaviour
    {
        private AudioSource _audioSource;
        [Inject] private SoundContainer.SoundContainer _soundContainer;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundEffect(SoundType sound)
        {
            if (!_soundContainer.SoundsStorage.TryGetValue(sound, out var clip))
            {
                Debug.LogError("Sound Type not found");
                return;
            }

            _audioSource.PlayOneShot(clip);
        }

        public void StopSoundEffect()
        {
            _audioSource.Stop();
        }
    }
}