using UnityEngine;
using Zenject;

namespace _ProjectFiles.SoundContainer
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioInitializer : MonoBehaviour
    {
        [SerializeField] private SoundType soundType;
        [Inject] private SoundContainer _soundContainer;
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            if (!_soundContainer.SoundsStorage.TryGetValue(soundType, out AudioClip clip))
            {
                Debug.LogError("Sound Type not found");
                return;
            }
            _audioSource.clip = clip;
        }
    }
}