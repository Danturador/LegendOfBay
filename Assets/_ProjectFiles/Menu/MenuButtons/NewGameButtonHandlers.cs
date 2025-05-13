using System.Collections;
using _ProjectFiles.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class NewGameButtonHandler : MenuButtonHandler
    {
        [SerializeField] private CutscenePlayer entryCutscene;
        [SerializeField] private GameObject menuParent;
        
        protected override void OnClick()
        {
            StartCoroutine(StartCutscene());
        }

        private IEnumerator StartCutscene()
        {
            yield return SceneTransition.Instance.Fade(1f);
            menuParent.SetActive(false);
            entryCutscene.Play(OnCutsceneEnd);
            var menuAudio = FindObjectOfType<AudioSource>();
            menuAudio.gameObject.SetActive(false);
            SceneTransition.Instance.FadeIn();
        }

        private void OnCutsceneEnd()
        {
            PlayerPrefs.SetInt(Storage.PrefsKey, 0);

            SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}