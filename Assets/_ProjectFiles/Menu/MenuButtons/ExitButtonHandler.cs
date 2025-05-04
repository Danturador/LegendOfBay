using UnityEditor;
using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class ExitGameButtonHandler : MenuButtonHandler
    {
        protected override void OnClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}