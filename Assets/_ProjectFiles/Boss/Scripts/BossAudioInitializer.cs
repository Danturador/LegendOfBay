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
	}

	private void PlaySound(SoundType sound)
	{
		if (!_soundContainer.SoundsStorage.TryGetValue(sound, out AudioClip clip))
		{
			Debug.LogError("Sound Type not found");
			return;
		}
		_audioSource.PlayOneShot(clip);
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
}