using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
	public static SceneTransition Instance;
	public float fadeDuration = 1f;
	[SerializeField] private Camera mainCamera;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}

		StartCoroutine(FadeIn());
		DontDestroyOnLoad(this);
	}

	public IEnumerator FadeIn()
	{
		float alpha = 1f;
		Color fadeColor = Color.black;

		while (alpha > 0f)
		{
			alpha -= Time.deltaTime / fadeDuration;
			SetCameraBackgroundColor(new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
			yield return null;
		}
		StartCoroutine(FadeOut());
	}
	public IEnumerator FadeOut()
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