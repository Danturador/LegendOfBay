using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun
{
    [CreateAssetMenu(menuName = "Enemy/Hundun/Attack Info", fileName = "AttackInfo")]
    public class HundunAttackInfo : EnemyAttackInfo
    {
        [SerializeField] private float attackStartDelay;
        public float AttackStartDelay => attackStartDelay;
    }
}