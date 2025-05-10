using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin
{
    [CreateAssetMenu(menuName = "Enemy/Kirin/Navigation Info", fileName = "NavigationInfo")]
    public class KirinNavigationInfo : EnemyNavigationInfo
    {
        [SerializeField] private float startDashDelay;
        [SerializeField] private float dashTimeInterval;
        [SerializeField] private float dashTime;
        [SerializeField] private float stopEdgeValue;
        [SerializeField] private AnimationCurve speedCurve;
        public float StartDashDelay => startDashDelay;
        public float DashTimeInterval => dashTimeInterval;
        public float DashTime => dashTime;
        public AnimationCurve SpeedCurve => speedCurve;
        public float StopEdgeValue => stopEdgeValue;
    }
}