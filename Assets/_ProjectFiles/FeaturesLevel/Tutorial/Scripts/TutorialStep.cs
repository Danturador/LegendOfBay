using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialStep : MonoBehaviour
{
	public Text textMesh; // replace with TextMeshProUGUI
	[TextArea] public string displayText;
	public float fadeDuration = 1f;

	private Coroutine currentFadeCoroutine;

	private void Awake()
	{
		textMesh.text = string.Empty;
		Color color = textMesh.color;
		color.a = 0f;
		textMesh.color = color;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			if (currentFadeCoroutine != null)
			{
				StopCoroutine(currentFadeCoroutine);
			}
			if (gameObject.activeInHierarchy)
			{
				currentFadeCoroutine = StartCoroutine(FadeIn());
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			if (currentFadeCoroutine != null)
			{
				StopCoroutine(currentFadeCoroutine);
			}
			if (gameObject.activeInHierarchy)
			{
				currentFadeCoroutine = StartCoroutine(FadeOut());
			}
		}
	}

	private IEnumerator FadeIn()
	{
		Color color = textMesh.color;
		textMesh.text = displayText;

		while (color.a < 1f)
		{
			color.a += Time.deltaTime / fadeDuration;
			textMesh.color = color;
			yield return null;
		}
	}

	private IEnumerator FadeOut()
	{
		Color color = textMesh.color;

		while (color.a > 0f)
		{
			color.a -= Time.deltaTime / fadeDuration;
			textMesh.color = color;
			yield return null;
		}

		textMesh.text = string.Empty;
	}
}