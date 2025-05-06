using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
	[SerializeField] private GameObject bossHumanForm;
	[SerializeField] private BossHealth humanFormHealthController;

	[SerializeField] private GameObject bossDemonicForm;
	[SerializeField] private BossHealth demonicFormHealthController;

	[SerializeField] private GameObject healthBarGO;
	[SerializeField] private Image healthBar;
	[SerializeField] private float fillDuration = 1f;


	[SerializeField] private BossDemonicFormStateMachine demonicFormBehaviour;
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
		healthBarGO.SetActive(false);
		bossDemonicForm.SetActive(false);
		isBossInactive = true;

		humanFormHealthController.HealthChanged += HandleHumanFormDeath;
		demonicFormHealthController.HealthChanged += HandleDemonFormDeath;
	}
	private void HandleHumanFormDeath(float currentHealth)
	{
		healthBar.fillAmount = currentHealth;
		if (currentHealth <= 0)
		{
			EnviromentAudioInitializer.Instance.PlayBossPhase2();
			StartCoroutine(TransitionToDemonicForm());
		}
	}
	private void HandleDemonFormDeath(float currentHealth)
	{
		healthBar.fillAmount = currentHealth;
		if (currentHealth <= 0)
		{
			demonicFormBehaviour.OnDeath?.Invoke();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() && isBossInactive)
		{
			EnviromentAudioInitializer.Instance.PlayBossPhase1();
			bossHumanForm.SetActive(true);
			healthBarGO.SetActive(true);
			isBossInactive = false;
		}
	}

	private IEnumerator TransitionToDemonicForm()
	{
		screenOverlay.color = new Color(1, 1, 1, 0);
		screenOverlay.gameObject.SetActive(true);
		float elapsedTime = 0f;

		StartCoroutine(FillHealthBar(0, 1));
		while (elapsedTime < transitionDuration)
		{
			float alpha = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
			screenOverlay.color = new Color(screenOverlay.color.r, screenOverlay.color.g, screenOverlay.color.b, alpha);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(transitionDelay);

		bossDemonicForm.SetActive(true);
		demonicFormCollider.enabled = false;

		while (elapsedTime < transitionDurationAfter)
		{
			float alpha = Mathf.Lerp(1, 0, elapsedTime / transitionDurationAfter);
			screenOverlay.color = new Color(screenOverlay.color.r, screenOverlay.color.g, screenOverlay.color.b, alpha);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		screenOverlay.gameObject.SetActive(false);
		StartCoroutine(Invulnerability());
	}

	private IEnumerator Invulnerability()
	{
		yield return new WaitForSeconds(invulnerabilityDuration);
		
		demonicFormBehaviour.InitializeDemonicForm();
		demonicFormCollider.enabled = true;
	}
	public IEnumerator FillHealthBar(float startFill, float endFill)
	{
		healthBar.fillAmount = startFill;

		float elapsedTime = 0f;
		float initialFill = startFill;
		float targetFill = endFill;

		while (elapsedTime < fillDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / fillDuration);
			healthBar.fillAmount = Mathf.Lerp(initialFill, targetFill, t);
			yield return null;
		}

		healthBar.fillAmount = targetFill;
	}
}