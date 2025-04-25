using System.Linq;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class HundunStateMachine : StateMachine
    {
        private readonly HundunActiveState _hundunActiveState;
        private bool _isVisibleByPlayer;

        public HundunStateMachine(EnemyProfile profile, EnemyContainer container) : base(container)
        {
            var activeState = (HundunActiveState)_states.ToList().First(x => x.GetType() == typeof(HundunActiveState));
            activeState.Container = _container;
        }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var transitions = new[]
            {
                new Transition(typeof(HundunPassiveState), typeof(HundunActiveState), CanChase),
                new Transition(typeof(HundunActiveState), typeof(HundunPassiveState), CannotChase),
                new Transition(typeof(HundunActiveState), typeof(HundunAttackState), CanAttack),
                new Transition(typeof(HundunAttackState), typeof(HundunActiveState), CannotAttack)
            };

            var states = new IState[] { new HundunPassiveState(), new HundunActiveState(), new HundunAttackState() };

            return (states, transitions);
        }

        private bool CanAttack()
        {
            return false;
        }

        private bool CannotAttack()
        {
            return true;
        }

        private bool CanChase()
        {
            return _container.IsVisibleByPlayer;
        }

        private bool CannotChase()
        {
            return !_container.IsVisibleByPlayer;
        }
    }
}