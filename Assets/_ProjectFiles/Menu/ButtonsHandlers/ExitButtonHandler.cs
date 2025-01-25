using UnityEditor;
using UnityEngine;

namespace _ProjectFiles.Menu.ButtonsHandlers
{
    public class ExitGameButtonHandler : ButtonHandler
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