using DG.Tweening;
using UnityEngine;

namespace _ProjectFiles.Menu.MenuButtons
{
    public class BackSettingButton : MenuButtonHandler
    {
        [SerializeField] private RectTransform bgImgRect;
        
        protected override void OnClick()
        {
            bgImgRect.DOAnchorPosX(0, 2f).SetEase(Ease.InOutExpo);
        }
    }
}