using _ProjectFiles.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class NewGameButtonHandler : MenuButtonHandler
    {
        protected override void OnClick()
        {
            PlayerPrefs.SetInt(Storage.PrefsKey, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}