using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Hundun
{

    [CreateAssetMenu(menuName = "Enemy/Hundun/Navigation info", fileName = "HundunNavigationInfo")]
    public class HundunNavigationInfo : EnemyNavigationInfo
    {
        [SerializeField] private float startDashDelay;
        [SerializeField] private float dashTimeInterval;
        [SerializeField] private AnimationCurve speedCurve;

        public float StartDashDelay => startDashDelay;
        public float DashTimeInterval => dashTimeInterval;
        public AnimationCurve SpeedCurve => speedCurve;
    }
}