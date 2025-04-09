using UnityEngine;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameResumeButtonHandler : InGameMenuButtonHandler
    {
        [SerializeField] private InGameMenu inGameMenu;

        protected override void OnClick()
        {
            inGameMenu.ToggleState();
        }
    }
}