using UnityEngine;
using UnityEngine.UI;

namespace _ProjectFiles.Menu
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
        
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}
