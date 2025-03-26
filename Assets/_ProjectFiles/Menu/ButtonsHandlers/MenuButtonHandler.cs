using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _ProjectFiles.Menu.ButtonsHandlers
{
    public abstract class MenuButtonHandler : ButtonHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //[SerializeField] private int fontSize;
        [FormerlySerializedAs("_buttonText")] [SerializeField]
        private Text buttonText;

        [FormerlySerializedAs("_textOutline")] [SerializeField]
        private Outline textOutline;

        private int _startFontSize;

        private void Awake()
        {
            textOutline.enabled = false;
            _startFontSize = 89;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonText.fontSize = 100;
            textOutline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonText.fontSize = 89;
            textOutline.enabled = false;
        }
    }
}