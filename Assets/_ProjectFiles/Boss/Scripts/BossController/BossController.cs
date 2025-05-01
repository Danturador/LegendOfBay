using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
	[SerializeField] private GameObject bossHumanForm;
	[SerializeField] private GameObject bossDemonicForm;
	[SerializeField] private BoxCollider2D demonicFormCollider;
	[SerializeField] private Image screenOverlay;
	[SerializeField] private float invulnerabilityDuration = 5f;
	[SerializeField] private float transitionDuration;
	[SerializeField] private float transitionDurationAfter;
	[SerializeField] private float transitionDelay;
	private bool isBossInactive;

	private void Awake()
	{
		bossHumanForm.SetActive(false);
		bossDemonicForm.SetActive(false);
		isBossInactive = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() && isBossInactive)
		{
			isBossInactive = false;
			StartCoroutine(TransitionToDemonicForm());
		}
	}

	private IEnumerator TransitionToDemonicForm()
	{
		screenOverlay.color = new Color(1, 1, 1, 0);
		screenOverlay.gameObject.SetActive(true);
		float elapsedTime = 0f;

		while (elapsedTime < transitionDuration)
		{
			float alpha = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
			screenOverlay.color = new Color(screenOverlay.color.r, screenOverlay.color.g, screenOverlay.color.b, alpha);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(transitionDelay);

		while (elapsedTime < transitionDurationAfter)
		{
			float alpha = Mathf.Lerp(1, 0, elapsedTime / transitionDurationAfter);
			screenOverlay.color = new Color(screenOverlay.color.r, screenOverlay.color.g, screenOverlay.color.b, alpha);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		screenOverlay.gameObject.SetActive(false);
		bossDemonicForm.SetActive(true);
		StartCoroutine(Invulnerability());
	}

	private IEnumerator Invulnerability()
	{
		demonicFormCollider.enabled = false;

		yield return new WaitForSeconds(invulnerabilityDuration);
		
		demonicFormCollider.enabled = true;
	}
}