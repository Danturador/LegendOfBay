using UnityEngine;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameSettingsButtonHandler : InGameMenuButtonHandler
    {
        [SerializeField] private GameObject panelToClose;
        [SerializeField] private GameObject panelToOpen;

        protected override void OnClick()
        {
            panelToClose.gameObject.SetActive(false);
            panelToOpen.gameObject.SetActive(true);
        }
    }
}