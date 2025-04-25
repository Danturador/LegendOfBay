using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin
{
    [CreateAssetMenu(menuName = "Enemy/Kirin/Attack Info", fileName = "AttackInfo")]
    public class KirinAttackInfo : EnemyAttackInfo
    {
        [SerializeField] private float attackStartDelay;
        [SerializeField] private float backAttackStartDelay;
        public float AttackStartDelay => attackStartDelay;
        public float BackAttackStartDelay => backAttackStartDelay;
    }
}