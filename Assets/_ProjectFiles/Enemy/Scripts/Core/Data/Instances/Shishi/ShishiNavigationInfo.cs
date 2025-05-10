using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi
{
    [CreateAssetMenu(menuName = "Enemy/Shishi/Navigation Info", fileName = "NavigationInfo")]
    public class ShishiNavigationInfo : EnemyNavigationInfo
    {
        [SerializeField] private float escapeRange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float stopEdgeValue;
        public float EscapeRange => escapeRange;
        public float MoveSpeed => moveSpeed;
        public float StopEdgeValue => stopEdgeValue;
    }
}