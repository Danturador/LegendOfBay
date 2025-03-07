using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _ProjectFiles.SoundContainer
{
    [CreateAssetMenu(fileName = "SoundContainer", menuName = "Sound Container")]
    public class SoundContainer : ScriptableObject
    {
        [SerializeField] private List<AudioModel> audios;
            
        public IReadOnlyDictionary<SoundType, AudioClip> SoundsStorage => 
            audios.ToDictionary(x => x.SoundType, x => x.SoundClip);
    }

    public enum SoundType
    {
        AttackClip
    }

    [Serializable]
    public class AudioModel
    {
        [field: SerializeField] public SoundType SoundType { get; private set; }
        [field: SerializeField] public AudioClip SoundClip { get; private set; }
    }
}