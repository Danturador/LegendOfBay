using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(fileName = "InfoContainer", menuName = "Enemy/Info container", order = 0)]
    public class EnemyInfoContainer : ScriptableObject
    {
        [SerializeField] private EnemyInfo enemyInfo;
        [SerializeField] private EnemyNavigationInfo navigationInfo;
        [SerializeField] private EnemyAttackInfo attackInfo;

        public EnemyInfo EnemyInfo => enemyInfo;
        public EnemyNavigationInfo NavigationInfo => navigationInfo;
        public EnemyAttackInfo AttackInfo => attackInfo;
    }
}