using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(fileName = "Profile", menuName = "Enemy/Profile")]
    public class EnemyProfile : ScriptableObject
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private EnemyNavigationInfo navigationInfo;
        [SerializeField] private EnemyAttackInfo attackInfo;

        public EnemyType Type => enemyType;
        public EnemyInfo EnemyInfo => enemyInfo;
        public EnemyNavigationInfo NavigationInfo => navigationInfo;
        public EnemyAttackInfo AttackInfo => attackInfo;
    }
}