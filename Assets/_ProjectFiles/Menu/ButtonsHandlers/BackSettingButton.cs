using DG.Tweening;
using UnityEngine;

namespace _ProjectFiles.Menu.ButtonsHandlers
{
    public class BackSettingButton : MenuButtonHandler
    {
        [SerializeField] private RectTransform img;
        
        protected override void OnClick()
        {
            img.DOAnchorPosX(0, 2f).SetEase(Ease.InOutExpo);
        }
    }
}