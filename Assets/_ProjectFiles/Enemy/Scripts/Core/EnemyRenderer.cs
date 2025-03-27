using Spine.Unity;
using System;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyRenderer : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SkeletonMecanim skeletonAnimation;
        private float _defaultScale;

        private void Awake()
        {
            _defaultScale = Mathf.Abs(skeletonAnimation.Skeleton.ScaleX);
        }

        private void Update()
        {
            var scale = rigidbody.velocity.x > 0 ? _defaultScale : -_defaultScale;
            skeletonAnimation.Skeleton.ScaleX = scale;

            // var scale = rigidbody.velocity.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            // transform.localScale = scale;
            //
            // Debug.Log(rigidbody.velocity.x > 0);
        }
    }
}
