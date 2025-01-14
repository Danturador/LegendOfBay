using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(menuName = "Enemy/Navigation info", fileName = "NavigationInfo")]
    public class EnemyNavigationInfo : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float provocationRadius;

        public float MoveSpeed => moveSpeed;
        public float ProvocationRadius => provocationRadius;
    }
}