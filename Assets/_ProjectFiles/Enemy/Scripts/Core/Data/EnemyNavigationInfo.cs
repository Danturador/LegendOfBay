using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(menuName = "Enemy/Navigation info", fileName = "NavigationInfo")]
    public class EnemyNavigationInfo : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float startDashDelay;
        [SerializeField] private float dashTimeInterval;
        [SerializeField] private float dashTime;
        [SerializeField] private Vector2 randomTargetOffset;

        public float MoveSpeed => moveSpeed;
        public float StartDashDelay => startDashDelay;
        public float DashTimeInterval => dashTimeInterval;
        public float DashTime => dashTime;
        public Vector2 RandomTargetOffset => randomTargetOffset;
    }
}