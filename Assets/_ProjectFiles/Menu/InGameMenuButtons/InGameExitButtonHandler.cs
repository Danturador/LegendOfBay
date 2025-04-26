using UnityEngine.SceneManagement;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameExitButtonHandler : ButtonHandler
    {
        protected override void OnClick()
        {
			SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}