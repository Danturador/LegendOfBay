using UnityEngine;
using UnityEngine.EventSystems;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public abstract class InGameMenuButtonHandler : ButtonHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly Color _hoverColor = new Color(1, 1, 1, 80 / 255f);
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

        private void ChangeState(bool showBg) => button.image.color = showBg ? _hoverColor : Color.clear;
    }
}