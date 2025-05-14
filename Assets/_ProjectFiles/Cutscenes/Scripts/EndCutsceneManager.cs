using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using _ProjectFiles.SaveSystem;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndCutsceneManager : MonoBehaviour
{
	[Inject] private SaveSystemController _saveSystem;
	[Inject] private InputController inputController;
	[SerializeField] private Image goodEnd;
	[SerializeField] private Image badEnd;

    [SerializeField] private Image fadeImage;
    [SerializeField] private Text fadeText;
    [SerializeField] private Image fadeTextBG;


    [SerializeField] private Image subtitlesImage;
    [SerializeField] private CutscenePlayer subtitlesCanvas;
    [SerializeField] private VideoPlayer videoPlayer;

	private bool canGoNext;
	private void Awake()
	{
		canGoNext = false;
		inputController.Gameplay.SmallAttack.performed += ctx => StartCoroutine(ShowSubtitles());
	}
		public void Launch()
	{
		StartCoroutine(StartCutscene());
	}
    private IEnumerator StartCutscene()
    {
		yield return StartCoroutine(FadeOut(2f));

		if (_saveSystem.gameData.SpawnersHolderData.spawnersData.Count > 0 && _saveSystem.gameData.SpawnersHolderData.spawnersData[0].isClosed)
		{
			goodEnd.gameObject.SetActive(true);
		}
		else 
		{
			badEnd.gameObject.SetActive(true);
		}

		yield return StartCoroutine(FadeIn(2f));
		StartCoroutine(FadeText(7f));
    }
	private IEnumerator ShowSubtitles()
	{
		if (canGoNext)
		{
			canGoNext = false;

			yield return StartCoroutine(FadeOut(2f));
			goodEnd.gameObject.SetActive(false);
			badEnd.gameObject.SetActive(false);
			subtitlesImage.gameObject.SetActive(true);

			//yield return StartCoroutine(WaitForVideoPreparation());
			yield return StartCoroutine(FadeIn(1f));

			subtitlesCanvas.Play(OnCutsceneEnd);
		}
	}

	private void OnCutsceneEnd()
	{
		SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
	public IEnumerator FadeOut(float duration)
    {
		fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(1, 1, 1, 0);
        float startAlpha = fadeImage.color.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(1, 1, 1, Mathf.Lerp(startAlpha, 1, t / duration));
            yield return null;
        }

        fadeImage.color = new Color(1, 1, 1, 1);
    }

    public IEnumerator FadeIn(float duration)
    {
		fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(1, 1, 1, 1);
        float startAlpha = fadeImage.color.a;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            fadeImage.color = new Color(1, 1, 1, Mathf.Lerp(startAlpha, 0, t / duration));
            yield return null;
        }

        fadeImage.color = new Color(1, 1, 1, 0);
    }

    public IEnumerator FadeText(float duration)
    {
		canGoNext = true;

        Color startColor = fadeText.color;
        Color startColorBG = fadeTextBG.color;
        startColor.a = 0;
		startColorBG.a = 0;
        fadeText.color = startColor;
		fadeTextBG.color = startColorBG;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0, 1, t / duration);
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, alpha);
			fadeTextBG.color = new Color(fadeTextBG.color.r, fadeTextBG.color.g, fadeTextBG.color.b, alpha);
            yield return null;
        }

        fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 1);
		fadeTextBG.color = new Color(fadeTextBG.color.r, fadeTextBG.color.g, fadeTextBG.color.b, 1);
    }
}
