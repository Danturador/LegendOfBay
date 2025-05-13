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
        PlayerAttackClip,
        PlayerRunClip1,
        PlayerRunClip2,
        PlayerRunClip3,
        PlayerRunClip4,
        PlayerRunClip5,
        PlayerHitEnemyClip1,
        PlayerHitEnemyClip2,
        PlayerHitEnemyClip3,
        BossSwordAttack1,
        BossSwordAttack2,
        BossSwordAttack3,
		AmbientStart,
		AmbientEnd,
		CaveClip,
		GatesClip,
		BossPhase1,
		BossPhase2,
		HitOnBoss,
		FireAttack,
		SwordAttack,
		SwordFallen,
		Death,
		
		KirinDamageTaken,
		KirinDash,
		KirinDeath,
		KirinRearing,
		KirinSprint,
		KirinSteps,
		
		ShishiActive,
		ShishiDamageTaken1,
		ShishiDamageTaken2,
		ShishiDeath,
		ShishiFire,
		ShishiFireLong,
		ShishiSteps,
    }

    [Serializable]
    public class AudioModel
    {
        [field: SerializeField] public SoundType SoundType { get; private set; }
        [field: SerializeField] public AudioClip SoundClip { get; private set; }
    }
}