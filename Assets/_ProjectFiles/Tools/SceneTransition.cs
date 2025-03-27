using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
	public float fadeDuration = 1f;
	private Camera mainCamera;

	private void Awake()
	{
		mainCamera = Camera.main;
		StartCoroutine(FadeIn());
		DontDestroyOnLoad(this);
	}

	private IEnumerator FadeIn()
	{
		float alpha = 1f;
		Color fadeColor = Color.black;

		while (alpha > 0f)
		{
			alpha -= Time.deltaTime / fadeDuration;
			SetCameraBackgroundColor(new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
			yield return null;
		}
	}

	public void StartFadeOut()
	{
		StartCoroutine(FadeOut());
	}

	private IEnumerator FadeOut()
	{
		float alpha = 0f;
		Color fadeColor = Color.black;

		while (alpha < 1f)
		{
			alpha += Time.deltaTime / fadeDuration;
			SetCameraBackgroundColor(new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
			yield return null;
		}
	}

	private void SetCameraBackgroundColor(Color color)
	{
		if (mainCamera != null)
		{
			mainCamera.backgroundColor = color;
		}
	}
}