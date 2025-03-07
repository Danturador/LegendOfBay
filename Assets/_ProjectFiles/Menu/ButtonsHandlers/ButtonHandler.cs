using System;
using System.Collections;
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
            StartCoroutine(InitButton());
        }

        private IEnumerator InitButton()
        {
            _button = GetComponent<Button>();
            _button.interactable = false;
            _button.onClick.AddListener(OnClick);
            yield return new WaitForSecondsRealtime(1.5f);
            _button.interactable = true;
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}
