using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(menuName = "Enemy/Info", fileName = "EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float provocationRadius;

        public float MaxHealth => maxHealth;
        public float ProvocationRadius => provocationRadius;
    }
}