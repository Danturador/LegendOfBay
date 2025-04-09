using UnityEngine;
using UnityEngine.EventSystems;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public abstract class InGameMenuButtonHandler : ButtonHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            button.image.color = Color.white;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            button.image.color = Color.clear;
        }
    }
}