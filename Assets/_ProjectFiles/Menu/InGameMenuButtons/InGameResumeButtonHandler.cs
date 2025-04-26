using UnityEngine;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameResumeButtonHandler : ButtonHandler
    {
        [SerializeField] private InGameMenu inGameMenu;

        protected override void OnClick()
        {
            inGameMenu.ToggleState();
        }
    }
}