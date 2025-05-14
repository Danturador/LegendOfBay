using _ProjectFiles.SoundContainer;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyAudio : MonoBehaviour
    {
        [SerializeField] private SoundContainer.SoundContainer soundContainer;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundEffect(SoundType sound)
        {
            if (!soundContainer.SoundsStorage.TryGetValue(sound, out var clip))
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