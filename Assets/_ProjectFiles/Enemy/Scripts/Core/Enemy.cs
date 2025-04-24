namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class Enemy
    {
        private EnemyAttackInfo _attackInfo;
        private EnemyInfo _enemyInfo;
        private EnemyNavigationInfo _navigationInfo;

        public Enemy(EnemyProfile profile, EnemyContainer container)
        {
            _enemyInfo = profile.EnemyInfo;
            _attackInfo = profile.AttackInfo;
            _navigationInfo = profile.NavigationInfo;

            State = new EnemyStateMachine(profile, container);
        }

        public EnemyStateMachine State { get; }
    }
}