using UnityEngine;
using System.Collections;
using Zenject;
using _ProjectFiles.SoundContainer;
using static _ProjectFiles.SoundContainer.SoundType;

[RequireComponent(typeof(AudioSource))]
public class EnviromentAudioInitializer : MonoBehaviour
{
	[SerializeField] private SoundType soundType;
	[SerializeField] private AudioClip currentClip;
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
				Debug.LogError("BossAudioInitializer Instance is null");
			}

			return _instance;
		}
	}
	public float fadeDuration = 2.0f;

	private bool _transitioned = false;
	private bool _inCave = false;

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
		currentClip = GetClip(AmbientStart);
		if (currentClip != null)
		{
			_ambientSource.clip = currentClip;
			_ambientSource.loop = true;
			//_ambientSource.volume = 1.0f;
			_ambientSource.Play();
		}
		else
		{
			Debug.LogWarning("Ambient Start Track is not assigned. No initial ambient sound will play.");
		}

		if (_oneShotSource == null)
		{
			_oneShotSource = gameObject.AddComponent<AudioSource>();
			Debug.LogWarning("OneShotSource was not assigned. Created a new AudioSource for one-shot sounds.");
		}
	}

	public void PlayAmbientStart()
	{
		if (_transitioned) return;
		
		_transitioned = true;
		StartCoroutine(FadeAndSwitch(GetClip(AmbientStart)));
	}
	public void PlayAmbientEnd()
	{
		if (_transitioned) return;

		_transitioned = true;
		StartCoroutine(FadeAndSwitch(GetClip(AmbientEnd)));
	}

	public void PlayGateOpenSound()
	{
		PlaySound(GatesClip);
	}

	public void EnterCave()
	{
		if (_inCave) return;

		_inCave = true;
		StartCoroutine(FadeAndSwitch(GetClip(CaveClip)));
	}

	public void ExitCave()
	{
		if (!_inCave) return;

		_inCave = false;
		StartCoroutine(FadeAndSwitch(GetClip(AmbientStart)));
	}

	private IEnumerator FadeAndSwitch(AudioClip newClip)
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
			_ambientSource.volume = Mathf.Lerp(0, 1.0f, time / fadeDuration);
			yield return null;
		}

		_ambientSource.volume = 1.0f;
	}
}