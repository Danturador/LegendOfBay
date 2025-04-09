using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _ProjectFiles.Menu.MenuButtons
{
    public abstract class MenuButtonHandler : ButtonHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Text buttonText;
        [SerializeField] private Outline textOutline;

        [SerializeField, Min(0)] private int fontStartSize;
        [SerializeField, Min(0)] private int fontBumpSize;

        private void Start()
        {
            textOutline.enabled = false;
            buttonText.fontSize = fontStartSize;
            StartCoroutine(InitButton());
        }
        
        private IEnumerator InitButton()
        {
            button.interactable = false;
            button.onClick.AddListener(OnClick);
            yield return new WaitForSecondsRealtime(1.5f);
            button.interactable = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonText.fontSize = fontBumpSize;
            textOutline.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttonText.fontSize = fontStartSize;
            textOutline.enabled = false;
        }
    }
}