using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(menuName = "Enemy/Navigation info", fileName = "NavigationInfo")]
    public class EnemyNavigationInfo : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float provocationRadius;
        [SerializeField] private float dashTimeInterval;
        [SerializeField] private float dashTime;

        public float MoveSpeed => moveSpeed;
        public float ProvocationRadius => provocationRadius;
        public float DashTimeInterval => dashTimeInterval;
        public float DashTime => dashTime;
    }
}