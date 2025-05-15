using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectFiles.Menu.DeathMenu
{
    public class DeathMenu : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField, Min(0f)] private float fadeDuration;

        public bool IsActive => backgroundImage.gameObject.activeInHierarchy;
        
        private void Awake()
        {
            backgroundImage.gameObject.SetActive(false);
        }
        
        public void ShowDeathMenu()
        {
            backgroundImage.color = Color.clear;
            backgroundImage.gameObject.SetActive(true);
            backgroundImage.DOFade(255, fadeDuration);
        }
    }
}