using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectFiles.Menu
{
    public class FadeAnimation : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField, Min(0f)] private float duration = 1f;
        private void Start()
        {
            img.DOFade(0, duration).SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
