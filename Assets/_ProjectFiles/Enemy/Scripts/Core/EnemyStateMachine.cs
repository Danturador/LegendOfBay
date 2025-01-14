namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyStateMachine : StateMachine
    {
        private readonly EnemyAttackInfo _attackInfo;
        private readonly EnemyNavigationInfo _navigationInfo;

        public EnemyStateMachine(EnemyAttackInfo attackInfo, EnemyNavigationInfo navigationInfo)
        {
            _attackInfo = attackInfo;
            _navigationInfo = navigationInfo;
        }

        public bool IsVisibleByPlayer { get; set; }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var states = new IState[] { new PassiveState(), new ActiveState(), new AttackState() };
            var transitions = new Transition[]
            {
                new(typeof(PassiveState), typeof(ActiveState), CanChase),
                new(typeof(ActiveState), typeof(PassiveState), CannotChase),
                new(typeof(ActiveState), typeof(AttackState), CanAttack)
            };

            return (states, transitions);
        }

        private bool CanAttack()
        {
            //var canAttack = DistanceFromPlayer <= _attackInfo.AttackRange;
            return false;
        }

        private bool CanChase()
        {
            var canChase = IsVisibleByPlayer;
            return canChase;
        }

        private bool CannotChase()
        {
            var canChase = !IsVisibleByPlayer;
            return canChase;
        }
    }
}