using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi
{
    [CreateAssetMenu(menuName = "Enemy/Shishi/Attack Info", fileName = "AttackInfo")]
    public class ShishiAttackInfo : EnemyAttackInfo
    {
        [SerializeField] private float missileSpeed;
        [SerializeField] private float missileFireTime;
        [SerializeField] private float missileRadius;

        public float MissileSpeed => missileSpeed;
        public float MissileFireTime => missileFireTime;
        public float MissileRadius => missileRadius;
    }
}