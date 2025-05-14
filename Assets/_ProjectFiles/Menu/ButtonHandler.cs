using System;
using _ProjectFiles.Menu.MenuButtons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace _ProjectFiles.Menu
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button _button;
        private AudioSource _audioSource;
        
        [Inject] protected ButtonsSoundContainer SoundContainer;
        
        protected virtual Color HoverColor => new(1, 1, 1, 80 / 255f);
           
        protected Button Btn
        {
            get
            {
                _button ??= GetComponent<Button>();
                return _button;
            }
        }
        
        protected AudioSource AudioSrc
        {
            get
            {
                _audioSource ??= GetComponent<AudioSource>();
                return _audioSource;
            }
        }

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _audioSource = GetComponent<AudioSource>();
            _button.onClick.AddListener(OnClick);
            _button.onClick.AddListener(PlaySoundOnClick);
        }

        protected void OnDisable()
        {
            ChangeState(false);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _button.onClick.RemoveListener(PlaySoundOnClick);
        }

        private void OnValidate()
        {
            _button = GetComponent<Button>();
            _audioSource = GetComponent<AudioSource>();
        }

        protected virtual void PlaySoundOnClick()
        {
            _audioSource.PlayOneShot(SoundContainer.clickSound);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeState(true);
            _audioSource.PlayOneShot(SoundContainer.hoverSound);
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