using UnityEngine.SceneManagement;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameExitButtonHandler : InGameMenuButtonHandler
    {
        protected override void OnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}