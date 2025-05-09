using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _ProjectFiles.Menu
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _button;
        protected virtual Color HoverColor => new(1, 1, 1, 80 / 255f);

        protected Button Btn
        {
            get
            {
                _button ??= GetComponent<Button>();
                return _button;
            }
        }

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        protected void OnDisable()
        {
            ChangeState(false);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeState(false);
        }

        private void ChangeState(bool showBg)
        {
            Btn.image.color = showBg ? HoverColor : Color.clear;
        }

        protected abstract void OnClick();
    }
}