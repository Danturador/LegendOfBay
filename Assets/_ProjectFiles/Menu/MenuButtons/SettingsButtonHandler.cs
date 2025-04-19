using DG.Tweening;
using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class SettingsButtonHandler : MenuButtonHandler
    {
        [SerializeField] private RectTransform img;

        protected override void OnClick()
        {
            img.DOAnchorPosX(1920, 2f).SetEase(Ease.InOutExpo);
        }
    }
}