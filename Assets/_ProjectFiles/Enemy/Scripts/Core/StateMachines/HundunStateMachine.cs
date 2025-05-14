using System.Linq;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class HundunStateMachine : StateMachine
    {
        private readonly HundunAttackInfo _attackInfo;
        private readonly HundunActiveState _hundunActiveState;
        private bool _isVisibleByPlayer;

        public HundunStateMachine(EnemyProfile profile, EnemyContainer container) : base(container)
        {
            var activeState = (HundunActiveState)_states.ToList().First(x => x.GetType() == typeof(HundunActiveState));
            activeState.Container = _container;

            _attackInfo = profile.AttackInfo as HundunAttackInfo;
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

            var states = new IState[]
                { new HundunPassiveState(), new HundunActiveState(), new HundunAttackState(_container) };

            return (states, transitions);
        }

        private bool CanAttack()
        {
            var targetDistance = Vector2.Distance(_container.transform.position,
                _container.Navigation.Target.transform.position);

            var canAttack = targetDistance < _attackInfo.AttackRange;
            return canAttack;
        }

        private bool CannotAttack()
        {
            return !CanAttack();
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