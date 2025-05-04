using DG.Tweening;
using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class SettingsButtonHandler : MenuButtonHandler
    {
        [SerializeField] private RectTransform bgImgRect;

        protected override void OnClick()
        {
            bgImgRect.DOAnchorPosX(-1920, 2f).SetEase(Ease.InOutExpo);
        }
    }
}