using DG.Tweening;
using UnityEngine;

namespace _ProjectFiles.Menu.ButtonsHandlers
{
    public class SettingsButtonHandler : ButtonHandler
    {
        private Camera _camera;
            
        private void Awake()
        {
            _camera = Camera.main;
        }

        protected override void OnClick()
        {
            _camera.transform.DOMoveX(-185, 2f).SetEase(Ease.InOutExpo);
        }
    }
}