using UnityEngine.SceneManagement;

namespace _ProjectFiles.Menu.DeathMenu
{
    public class RestartGameButton : ButtonHandler
    {
        protected override void OnClick()
        {
            SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
