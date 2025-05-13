using UnityEngine;
using System.Collections;
using Zenject;
using _ProjectFiles.SoundContainer;
using static _ProjectFiles.SoundContainer.SoundType;

[RequireComponent(typeof(AudioSource))]
public class EnviromentAudioInitializer : MonoBehaviour
{
	[SerializeField] private SoundType soundType;
	[Inject] private SoundContainer _soundContainer;
	[SerializeField] private AudioSource _ambientSource;
	[SerializeField] private AudioSource _oneShotSource;

	private static EnviromentAudioInitializer _instance;
	public static EnviromentAudioInitializer Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError("EnviromentAudioInitializer Instance is null");
			}

			return _instance;
		}
	}
	public float fadeDuration = 2.0f;

	private bool _inCave = false;
	private Coroutine _currentFadeCoroutine;

	private void Awake()
	{
		_instance = this;
	}
	private void PlaySound(SoundType sound)
	{
		AudioClip clip = GetClip(sound);
		if (clip != null)
		{
			_oneShotSource.PlayOneShot(clip);
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
	private void Start()
	{
		_ambientSource.clip = GetClip(AmbientStart);
		_ambientSource.loop = true;
//		_ambientSource.volume = 1.0f;
		_ambientSource.Play();


		if (_oneShotSource == null)
		{
			_oneShotSource = gameObject.AddComponent<AudioSource>();
			Debug.LogWarning("OneShotSource was not assigned. Created a new AudioSource for one-shot sounds.");
		}
	}

	public void PlayAmbient(SoundType soundType)
	{
		StartFadeAndSwitch(GetClip(soundType));
	}
	public void PlayBossPhase1()
	{
		StartFadeAndSwitch(GetClip(BossPhase1));
	}
	public void PlayBossPhase2()
	{
		StartFadeAndSwitch(GetClip(BossPhase2));
	}

	public void PlayGateOpenSound()
	{
		PlaySound(GatesClip);
	}

	public void EnterCave()
	{
		if (_inCave) return;

		_inCave = true;
		StartFadeAndSwitch(GetClip(CaveClip));
	}

	public void ExitCave()
	{
		if (!_inCave) return;

		_inCave = false;
		StartFadeAndSwitch(GetClip(AmbientStart));
	}

	private void StartFadeAndSwitch(AudioClip newClip)
	{
		if (_currentFadeCoroutine != null)
		{
			StopCoroutine(_currentFadeCoroutine);
		}

		_currentFadeCoroutine = StartCoroutine(FadeAndSwitch(newClip));
	}

	public IEnumerator FadeAndSwitch(AudioClip newClip)
	{
		float startVolume = _ambientSource.volume;

		float time = 0;
		while (time < fadeDuration)
		{
			time += Time.deltaTime;
			_ambientSource.volume = Mathf.Lerp(startVolume, 0, time / fadeDuration);
			yield return null;
		}

		_ambientSource.Stop();
		_ambientSource.clip = newClip;
		_ambientSource.volume = 0;
		_ambientSource.Play();

		time = 0;
		while (time < fadeDuration)
		{
			time += Time.deltaTime;
			_ambientSource.volume = Mathf.Lerp(0, 1.0f, time / fadeDuration); // !
			yield return null;
		}

		_ambientSource.volume = 1.0f; //!
		_currentFadeCoroutine = null;
	}
}