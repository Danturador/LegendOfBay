using _ProjectFiles.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ProjectFiles.Menu.ButtonsHandlers
{
    public class ResumeGameButtonHandler : MenuButtonHandler
    {
        protected override void OnClick()
        {
            PlayerPrefs.SetInt(Storage.PrefsKey, 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}