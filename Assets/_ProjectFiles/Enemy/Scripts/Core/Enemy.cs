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

            switch (profile.EnemyInfo.Type)
            {
                case EnemyType.Hundun:
                {
                    State = new HundunStateMachine(profile, container);
                    break;
                }
                    
                case EnemyType.Shishi:
                {
                    State = new ShishiStateMachine(profile, container);
                    break;
                }
                
                case EnemyType.Kirin:
                {
                    State = new KirinStateMachine(profile, container);
                    break;
                }
            }
            
          
        }

        public StateMachine State { get; }
    }
}