using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class VolumeSlider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const string MusicVolume = "MusicVolume";
        private const string SfxVolume = "SFXVolume";
        
        private const string VolumeValueKey = "VolumeValue";
        
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private Slider slider;
        [SerializeField] private Image backLight;
        
        private void Awake()
        {
            slider.onValueChanged.AddListener(VolumeChanged);
        }

        private void OnEnable()
        {
            backLight.gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            backLight.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(VolumeChanged);
        }

        public void LoadVolumeValue()
        {
            float value = PlayerPrefs.GetFloat(VolumeValueKey, 1);
            slider.value = value;
            VolumeChanged(value);
        }
        
        private void VolumeChanged(float value)
        {
            mixer.SetFloat(MusicVolume, Mathf.Log10(value) * 20);
            mixer.SetFloat(SfxVolume, Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(VolumeValueKey, value);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            backLight.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            backLight.gameObject.SetActive(false);
        }
    }
}
