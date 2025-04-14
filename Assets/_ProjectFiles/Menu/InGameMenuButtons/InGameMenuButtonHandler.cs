using UnityEngine;
using UnityEngine.EventSystems;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public abstract class InGameMenuButtonHandler : ButtonHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private void OnDisable()
        {
            ChangeState(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeState(false);
        }

        private void ChangeState(bool showBg) => button.image.color = showBg ? Color.white : Color.clear;
    }
}