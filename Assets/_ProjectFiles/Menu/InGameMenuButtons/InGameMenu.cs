using UnityEngine;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] [Min(0f)] private float fadeDuration;
        private Transform _child;
        //private Image _background;
        //private bool _canToggle;

        private void Awake()
        {
            _child = transform.GetChild(0);
            _child.gameObject.SetActive(false);
            //_background = _child.gameObject.GetComponent<Image>();
            //_canToggle = true;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            ToggleState();
        }

        public void ToggleState()
        {
            // bool isActive = _child.gameObject.activeInHierarchy;
            // float endValue = isActive ? 0f : 0.85f;
            //
            // if (!isActive)
            // {
            //     _background.color = Color.clear;
            //     _child.gameObject.SetActive(true);
            // }
            //     
            // _background.DOFade(endValue, fadeDuration).SetEase(Ease.Linear)
            //     .SetUpdate(true).OnComplete(() =>
            //     {
            //         _canToggle = true;
            //         if (isActive)
            //         {
            //             _background.color = Color.clear;
            //             _child.gameObject.SetActive(false);
            //         }
            //     });
            var isActive = _child.gameObject.activeInHierarchy;
            _child.gameObject.SetActive(!isActive);
            ResetMenu();
        }

        private void ResetMenu()
        {
            _child.GetChild(0).gameObject.SetActive(true);
            _child.GetChild(1).gameObject.SetActive(false);
        }
    }
}