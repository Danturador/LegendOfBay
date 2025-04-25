using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyAttackInfo : ScriptableObject
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackDelay;
        [SerializeField] private int damage;

        public float AttackRange => attackRange;
        public float AttackDelay => attackDelay;
        public int Damage => damage;
    }
}