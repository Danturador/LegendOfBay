using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    public abstract class MenuButtonHandler : ButtonHandler
    {
        protected override Color HoverColor => new(1, 1, 1, 170 / 255f);
           
        private void Start()
        {
            StartCoroutine(InitButton());
        }

        private IEnumerator InitButton()
        {
            Btn.interactable = false;
            yield return new WaitForSecondsRealtime(1.5f);
            Btn.interactable = true;
        }
    }
}