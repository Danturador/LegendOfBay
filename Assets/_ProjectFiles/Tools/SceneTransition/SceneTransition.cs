using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    public Image fadeImage;
    public Image hundunImage;
    public AudioSource menuAudio;
    public float fadeDuration = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
        hundunImage.gameObject.SetActive(false);
        menuAudio = FindObjectOfType<AudioSource>();
        FadeIn();
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        yield return Fade(1f);
        hundunImage.gameObject.SetActive(true);
        var operation = SceneManager.LoadSceneAsync(sceneIndex);
        
        while (!operation.isDone) yield return null;
        
        hundunImage.gameObject.SetActive(false);
        yield return Fade(0f);
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0f));
    }
    
    public void FadeOut()
    {
        StartCoroutine(Fade(0f));
    }

    private void InstantFade()
    {
        var finalColor = fadeImage.color;
        finalColor.a = 1f;
        fadeImage.color = finalColor;
    }

    public IEnumerator Fade(float targetAlpha)
    {
        var startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            var alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            var color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
			if (menuAudio != null)
			{
				menuAudio.volume = 1 - alpha;
			}
            yield return null;
        }

        var finalColor = fadeImage.color;
        finalColor.a = targetAlpha;
        fadeImage.color = finalColor;
    }
}