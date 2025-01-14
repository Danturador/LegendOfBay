using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(menuName = "Enemy/Attack info", fileName = "AttackInfo")]
    public class EnemyAttackInfo : ScriptableObject
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackSpeed;
        [SerializeField] private int damage;

        public float AttackRange => attackRange;
        public float AttackSpeed => attackSpeed;
        public float Damage => damage;
    }
}