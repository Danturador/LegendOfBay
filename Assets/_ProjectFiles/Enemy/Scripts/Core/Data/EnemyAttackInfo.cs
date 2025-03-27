using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
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