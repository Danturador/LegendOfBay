using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using _ProjectFiles.SoundContainer;
using static _ProjectFiles.SoundContainer.SoundType;

[RequireComponent(typeof(AudioSource))]
public class BossAudioInitializer : MonoBehaviour
{
	[SerializeField] private SoundType soundType;
	[Inject] private SoundContainer _soundContainer;
	private AudioSource _audioSource;
	private bool isHitPlaying;

	private static BossAudioInitializer _instance;
	public static BossAudioInitializer Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError("BossAudioInitializer Instance is null");
			}

			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		_audioSource = GetComponent<AudioSource>();

		isHitPlaying = false;
	}
	public void Deinitialize()
	{
		_audioSource.Stop();
		_audioSource.enabled = false;
	}
	public void PlaySound(SoundType sound)
	{
		AudioClip clip = GetClip(sound);
		if (clip != null)
		{
			_audioSource.PlayOneShot(clip);
		}
	}
	private AudioClip GetClip(SoundType sound)
	{
		if (_soundContainer.SoundsStorage.TryGetValue(sound, out AudioClip clip))
		{
			return clip;
		}
		return null;
	}

	public void PlaySwordAttack(int attackIndex)
	{
		switch (attackIndex)
		{
			case 1:
				soundType = BossSwordAttack1;
				break;
			case 2:
				soundType = BossSwordAttack2;
				break;
			case 3:
				soundType = BossSwordAttack3;
				break;
			default:
				break;
		}
		PlaySound(soundType);
	}
	public void PlayHitOnBoss()
	{
		if (!isHitPlaying)
		{
			StartCoroutine(PlayHitOnBossSeq());
		}
	}
	private IEnumerator PlayHitOnBossSeq()
	{
		isHitPlaying = true;

		yield return new WaitForSeconds(GetClip(HitOnBoss).length);

		PlaySound(HitOnBoss);
		isHitPlaying = false;
	}
}