using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialStep : MonoBehaviour
{
	public Text text;
	public Image image; // Reference to the image that will fade in and out
	[TextArea] public string displayText;
	public float fadeDuration = 1f;
	public float textDelay = 0.2f;

	private Coroutine currentFadeCoroutine;
	private Coroutine currentFadeCoroutineImage;

	private void Awake()
	{
		// Initialize text and image transparency
		text.text = string.Empty;
		Color textColor = text.color;
		textColor.a = 0f;
		text.color = textColor;

		Color imageColor = image.color;
		imageColor.a = 0f;
		image.color = imageColor;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			if (currentFadeCoroutine != null)
			{
				StopCoroutine(currentFadeCoroutine);
			}
			if (currentFadeCoroutineImage != null)
			{
				StopCoroutine(currentFadeCoroutineImage);
			}
			if (gameObject.activeInHierarchy)
			{
				currentFadeCoroutineImage = StartCoroutine(FadeInImage());
				currentFadeCoroutine = StartCoroutine(FadeInText());
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
			if (currentFadeCoroutineImage != null)
			{
				StopCoroutine(currentFadeCoroutineImage);
			}
			if (gameObject.activeInHierarchy)
			{
				currentFadeCoroutine = StartCoroutine(FadeOutText());
				currentFadeCoroutineImage = StartCoroutine(FadeOutImage());
			}
		}
	}

	private IEnumerator FadeInImage()
	{
		Color color = image.color;

		while (color.a < 1f)
		{
			color.a += Time.deltaTime / fadeDuration;
			image.color = color;
			yield return null;
		}
	}

	private IEnumerator FadeInText()
	{
		Color color = text.color;
		text.text = displayText;

		yield return new WaitForSeconds(textDelay);

		while (color.a < 1f)
		{
			color.a += Time.deltaTime / fadeDuration;
			text.color = color;
			yield return null;
		}
	}

	private IEnumerator FadeOutText()
	{
		Color color = text.color;

		while (color.a > 0f)
		{
			color.a -= Time.deltaTime / fadeDuration;
			text.color = color;
			yield return null;
		}

		text.text = string.Empty;
	}

	private IEnumerator FadeOutImage()
	{
		Color color = image.color;

		yield return new WaitForSeconds(textDelay * 2);

		while (color.a > 0f)
		{
			color.a -= Time.deltaTime / fadeDuration;
			image.color = color;
			yield return null;
		}
	}
}