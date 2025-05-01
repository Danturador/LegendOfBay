using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun
{
    [CreateAssetMenu(menuName = "Enemy/Hundun/Navigation Info", fileName = "NavigationInfo")]
    public class HundunNavigationInfo : EnemyNavigationInfo
    {
        [SerializeField] private float startDashDelay;
        [SerializeField] private float dashTimeInterval;
        [SerializeField] private float dashTime;
        [SerializeField] private Vector2 randomTargetOffset;
        [SerializeField] private AnimationCurve speedCurve;

        public float StartDashDelay => startDashDelay;
        public float DashTimeInterval => dashTimeInterval;
        public float DashTime => dashTime;
        public Vector2 RandomTargetOffset => randomTargetOffset;
        public AnimationCurve SpeedCurve => speedCurve;
    }
}