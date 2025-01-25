using System;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectFiles.Menu.ButtonsHandlers
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonHandler : MonoBehaviour
    {
        private Button _button;

        public Button button 
        { 
            get
            {
                _button ??= GetComponent<Button>();
                return _button;
            }
        }


        protected virtual void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}
