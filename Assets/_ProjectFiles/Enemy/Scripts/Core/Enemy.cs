namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class Enemy
    {
        private EnemyAttackInfo _attackInfo;
        private EnemyInfo _enemyInfo;
        private EnemyNavigationInfo _navigationInfo;

        public Enemy(EnemyInfoContainer infoContainer, EnemyContainer container)
        {
            _enemyInfo = infoContainer.EnemyInfo;
            _attackInfo = infoContainer.AttackInfo;
            _navigationInfo = infoContainer.NavigationInfo;

            State = new EnemyStateMachine(infoContainer, container);
        }

        public EnemyStateMachine State { get; }
    }
}