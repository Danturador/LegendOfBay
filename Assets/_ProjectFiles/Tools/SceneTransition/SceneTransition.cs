using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
	public Image fadeImage;
	public float fadeDuration = 2f;

	private void Start()
	{
		fadeImage.gameObject.SetActive(true);

		FadeIn();
	}

	public void LoadScene(string sceneName)
	{
		StartCoroutine(FadeOutCoroutine(sceneName));
	}

	private IEnumerator FadeOutCoroutine(string sceneName)
	{
		yield return Fade(1f);
		SceneManager.LoadScene(sceneName);
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