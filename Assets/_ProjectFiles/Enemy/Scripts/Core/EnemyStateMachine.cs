using System.Linq;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyStateMachine : StateMachine
    {
        private readonly ActiveState _activeState;
        private readonly EnemyAttackInfo _attackInfo;
        private readonly EnemyContainer _container;
        private readonly EnemyNavigationInfo _navigationInfo;

        private bool _isVisibleByPlayer;

        public EnemyStateMachine(EnemyInfoContainer infoContainer, EnemyContainer container)
        {
            _attackInfo = infoContainer.AttackInfo;
            _navigationInfo = infoContainer.NavigationInfo;
            _container = container;

            var activeState = (ActiveState)_states.ToList().First(x => x.GetType() == typeof(ActiveState));
            activeState.Container = container;
        }

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
            return false;
        }

        private bool CanChase()
        {
            var canChase = _isVisibleByPlayer;
            return canChase;
        }

        private bool CannotChase()
        {
            var canChase = !_isVisibleByPlayer;
            return canChase;
        }

        public void SetVisibility(bool isVisible)
        {
            _isVisibleByPlayer = isVisible;
        }
    }
}