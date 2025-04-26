using System.Linq;
using _ProjectFiles.Enemy.Scripts.Behaviour.States.Kirin;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class KirinStateMachine : StateMachine
    {
        private readonly KirinAttackInfo _attackInfo;
        private readonly KirinActiveState _hundunActiveState;
        private bool _isVisibleByPlayer;

        public KirinStateMachine(EnemyProfile profile, EnemyContainer container) : base(container)
        {
            var activeState = (KirinActiveState)_states.ToList().First(x => x.GetType() == typeof(KirinActiveState));
            activeState.Container = _container;
            _attackInfo = _container.Profile.AttackInfo as KirinAttackInfo;
        }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var transitions = new[]
            {
                new Transition(typeof(KirinPassiveState), typeof(KirinActiveState), CanChase),
                new Transition(typeof(KirinActiveState), typeof(KirinPassiveState), CannotChase),
                new Transition(typeof(KirinActiveState), typeof(KirinAttackState), CanAttack),
                new Transition(typeof(KirinAttackState), typeof(KirinActiveState), CannotAttack)
            };

            var states = new IState[]
                { new KirinPassiveState(), new KirinActiveState(), new KirinAttackState(_container) };

            return (states, transitions);
        }

        private bool CanAttack()
        {
            if (_container.Navigation.Target == null) return false;

            var targetDistance =
                Mathf.Abs(_container.Navigation.Target.transform.position.x - _container.transform.position.x);
            return targetDistance < _attackInfo.AttackRange;
        }

        private bool CanChase()
        {
            return _container.IsVisibleByPlayer;
        }

        private bool CannotAttack()
        {
            return !CanAttack();
        }

        private bool CannotChase()
        {
            return !CanChase();
        }
    }
}