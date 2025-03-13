using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialStep : MonoBehaviour
{
	public Text textMesh;
	[TextArea] public string displayText;
	public float fadeDuration = 1f;
	//private bool isEntered;

	private void Awake()
	{
		textMesh.text = string.Empty;
		Color color = textMesh.color;
		color.a = 0f;
		textMesh.color = color;

		//isEntered = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			StartCoroutine(FadeIn());
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			StartCoroutine(FadeOut());
		}
	}

	private IEnumerator FadeIn()
	{
		//isEntered = true;

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

		//isEntered = false;
	}
}