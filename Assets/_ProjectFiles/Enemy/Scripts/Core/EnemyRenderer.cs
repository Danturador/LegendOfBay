using Spine.Unity;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyRenderer : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SkeletonMecanim skeletonAnimation;
        [SerializeField] private float velocityError;
        private float _defaultScale;

        public float CurrentScale
        {
            get => skeletonAnimation.Skeleton.ScaleX;
            set => skeletonAnimation.Skeleton.ScaleX = value;
        }

        private void Awake()
        {
            _defaultScale = Mathf.Abs(skeletonAnimation.Skeleton.ScaleX);
        }

        private void Update()
        {
            if (rigidbody.velocity.x > velocityError)
                skeletonAnimation.Skeleton.ScaleX = _defaultScale;
            else if (rigidbody.velocity.x < -velocityError) skeletonAnimation.Skeleton.ScaleX = -_defaultScale;
        }
    }
}