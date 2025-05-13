using System.Collections;
using _ProjectFiles.SoundContainer;
using UnityEngine;
using Zenject;

public class DestroyableSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [Inject] private SoundContainer _soundContainer;
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(DestroyAfterAudio(clip.length));
    }
    
    private IEnumerator DestroyAfterAudio(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
