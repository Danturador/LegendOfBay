using System.Linq;
using _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class ShishiStateMachine : StateMachine
    {
        private readonly ShishiNavigationInfo _navigationInfo;
        private readonly ShishiActiveState _hundunActiveState;
        private bool _isVisibleByPlayer;

        public ShishiStateMachine(EnemyProfile profile, EnemyContainer container) : base(container)
        {
            var activeState = (ShishiActiveState)_states.ToList().First(x => x.GetType() == typeof(ShishiActiveState));
            activeState._container = _container;
            _navigationInfo = _container.Profile.NavigationInfo as ShishiNavigationInfo;
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
                { new ShishiPassiveState(), new ShishiActiveState(_container), new ShishiAttackState() };

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
            return _container.IsVisibleByPlayer;
        }

        private bool CannotEscape()
        {
            return !CanEscape();
        }

        private bool CannotAttack()
        {
            return !CanAttack();
        }
    }
}