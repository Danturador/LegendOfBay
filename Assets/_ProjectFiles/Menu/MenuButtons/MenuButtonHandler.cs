using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    public abstract class MenuButtonHandler : ButtonHandler
    {
        private void Start()
        {
            StartCoroutine(InitButton());
        }

        private IEnumerator InitButton()
        {
            Btn.interactable = false;
            Btn.onClick.AddListener(OnClick);
            yield return new WaitForSecondsRealtime(1.5f);
            Btn.interactable = true;
        }
    }
}