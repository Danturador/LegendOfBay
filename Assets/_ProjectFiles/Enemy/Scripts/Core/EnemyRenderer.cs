using Spine.Unity;
using System;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyRenderer : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private SkeletonMecanim skeletonAnimation;
        [SerializeField] private Animator animator;
        private float _defaultScale;

        private void Awake()
        {
            _defaultScale = Mathf.Abs(skeletonAnimation.Skeleton.ScaleX);
        }

        private void Update()
        {
            var scale = rigidbody.velocity.x > 0 ? _defaultScale : -_defaultScale;
            skeletonAnimation.Skeleton.ScaleX = scale;
        }

        public void SetActiveStateEnabled(bool enabled)
        {
         animator.SetBool("activeState", enabled);   
        }
        
        public void SetSAttackStateEnabled(bool enabled)
        {
            animator.SetBool("attackState", enabled);   
        }
        
        public void SetPassiveStateEnabled(bool enabled)
        {
            animator.SetBool("passiveState", enabled);   
        }
    }
}
