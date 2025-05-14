using _ProjectFiles.SoundContainer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioInitializer : MonoBehaviour
{
    [SerializeField] private SoundType soundType;
    [Inject] private SoundContainer _soundContainer;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySoundEffect(SoundType sound)
    {

        if (!_soundContainer.SoundsStorage.TryGetValue(sound, out AudioClip clip))
        {
            Debug.LogError("Sound Type not found");
            return;
        }
       // _audioSource.clip = clip;
        _audioSource.PlayOneShot(clip);
    }

    public void PlayerRunSound(int numberClip)
    {
        if (numberClip == 1) 
        {
            soundType = SoundType.PlayerRunClip1;
            PlaySoundEffect(soundType);
        } else if (numberClip == 2) 
        {
            soundType = SoundType.PlayerRunClip2;
            PlaySoundEffect(soundType);
        }
        else
        {
            soundType = SoundType.PlayerRunClip3;
            PlaySoundEffect(soundType);
        }
    }

    public void PlayerHitEnemySound(int numberClip)
    {
        if (numberClip == 1)
        {
            soundType = SoundType.PlayerHitEnemyClip1;
            PlaySoundEffect(soundType);
        }
        else if (numberClip == 2)
        {
            soundType = SoundType.PlayerHitEnemyClip2;
            PlaySoundEffect(soundType);
        }
        else
        {
            soundType = SoundType.PlayerHitEnemyClip3;
            PlaySoundEffect(soundType);
        }
    }

    public void PlayerAttackSound(int numberClip)
    {
        if (numberClip == 1)
        {
            soundType = SoundType.PlayerAttackClip1;
            PlaySoundEffect(soundType);
        }
        else if (numberClip == 2)
        {
            soundType = SoundType.PlayerAttackClip2;
            PlaySoundEffect(soundType);
        }
        else
        {
            soundType = SoundType.PlayerAttackClip3;
            PlaySoundEffect(soundType);
        }
    }

    public void PlayerDashSound()
    {
        soundType = SoundType.PlayerDash;
        PlaySoundEffect(soundType);
    }

    public void PlayerGrapplingHookSound()
    {
        soundType = SoundType.PlayerGraplingHook;
        PlaySoundEffect(soundType);
    }

    public void PlayerJumpSound()
    {
        soundType = SoundType.PlayerJump;
        PlaySoundEffect(soundType);
    }

    public void PlayerDoubleJumpSound()
    {
        soundType = SoundType.PlayerDoubleJump;
        PlaySoundEffect(soundType);
    }
}
