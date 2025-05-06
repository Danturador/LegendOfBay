using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
	public static SceneTransition Instance;
	public Image fadeImage;
	public Image hundunImage;
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
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		while (!operation.isDone)
		{
			yield return null;
		}

		hundunImage.gameObject.SetActive(false);
		yield return Fade(0f);
	}

	public void FadeIn()
	{
		StartCoroutine(Fade(0f));
	}

	private IEnumerator Fade(float targetAlpha)
	{
		float startAlpha = fadeImage.color.a;
		float time = 0;

		while (time < fadeDuration)
		{
			time += Time.deltaTime;
			float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
			Color color = fadeImage.color;
			color.a = alpha;
			fadeImage.color = color;
			yield return null;
		}

		Color finalColor = fadeImage.color;
		finalColor.a = targetAlpha;
		fadeImage.color = finalColor;
	}
}