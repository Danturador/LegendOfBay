namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class Enemy
    {
        private EnemyAttackInfo _attackInfo;
        private EnemyInfo _enemyInfo;
        private EnemyNavigationInfo _navigationInfo;

        public Enemy(EnemyInfo enemyInfo, EnemyAttackInfo attackInfo, EnemyNavigationInfo navigationInfo)
        {
            _enemyInfo = enemyInfo;
            _attackInfo = attackInfo;
            _navigationInfo = navigationInfo;

            State = new EnemyStateMachine(attackInfo, navigationInfo);
        }

        public EnemyStateMachine State { get; }
    }
}