using System.Linq;
using _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class ShishiStateMachine : StateMachine
    {
        private readonly ShishiActiveState _activeState;
        private readonly ShishiNavigationInfo _navigationInfo;
        private readonly ShishiAttackInfo _attackInfo;
        private readonly ShishiAttack _attack;
        private bool _isVisibleByPlayer;

        public ShishiStateMachine(EnemyProfile profile, EnemyContainer container) : base(container)
        {
            var activeState = (ShishiActiveState)_states.ToList().First(x => x.GetType() == typeof(ShishiActiveState));
            activeState._container = _container;
            _navigationInfo = _container.Profile.NavigationInfo as ShishiNavigationInfo;
            _attackInfo = _container.Profile.AttackInfo as ShishiAttackInfo;
            _attack = container.Attack.AttackExecutable as ShishiAttack;
        }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var transitions = new[]
            {
                new Transition(typeof(ShishiPassiveState), typeof(ShishiAttackState), CanAttack),
                new Transition(typeof(ShishiAttackState), typeof(ShishiPassiveState), CannotAttack),
                new Transition(typeof(ShishiActiveState), typeof(ShishiAttackState), CannotEscape),
                new Transition(typeof(ShishiAttackState), typeof(ShishiActiveState), CanEscape)
            };

            var states = new IState[]
                { new ShishiPassiveState(_container), new ShishiActiveState(_container), new ShishiAttackState(_container) };

            return (states, transitions);
        }

        private bool CanEscape()
        {
            if (_container.Navigation.Target == null) return false;

            var targetDistance =
                Mathf.Abs(_container.Navigation.Target.transform.position.x - _container.transform.position.x);
            return targetDistance < _navigationInfo.EscapeRange;
        }

        private bool CanAttack()
        {
            var target  = _container.Navigation.Target;
            var targetDelta = _container.transform.position.x - target.position.x;
            var result = _container.IsVisibleByPlayer && Mathf.Abs(targetDelta) < _attackInfo.AttackRange;
            return result;
        }

        private bool CannotEscape()
        {
            return !CanEscape();
        }

        private bool CannotAttack()
        {
            return !CanAttack() && !_attack.IsAttacking;
        }
    }
}