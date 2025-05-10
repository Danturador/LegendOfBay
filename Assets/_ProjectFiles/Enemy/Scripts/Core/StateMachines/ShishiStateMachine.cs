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
        private readonly ShishiAttack _attack;
        private readonly ShishiAttackInfo _attackInfo;
        private readonly ShishiNavigationInfo _navigationInfo;
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
                new Transition(typeof(ShishiAttackState), typeof(ShishiPassiveState), CanStopAttack),
                new Transition(typeof(ShishiActiveState), typeof(ShishiAttackState), CanStopEscape),
                new Transition(typeof(ShishiAttackState), typeof(ShishiActiveState), CanEscape),
            };

            var states = new IState[]
            {
                new ShishiPassiveState(_container), new ShishiActiveState(_container), new ShishiAttackState(_container)
            };

            return (states, transitions);
        }

        private bool CanEscape()
        {
            var isOutOfBounds = IsOutOfBounds();

            if (isOutOfBounds)
            {
                return false;
            }

            if (_container.Navigation.Target == null) return false;

            var targetDistance =
                Mathf.Abs(_container.Navigation.Target.transform.position.x - _container.transform.position.x);

            
            return targetDistance < _navigationInfo.EscapeRange;
        }

        private bool CanAttack()
        {
            var target = _container.Navigation.Target;

            if (target == null) return false;
            var targetDelta = _container.transform.position.x - target.transform.position.x;
            var canAttack = _container.IsVisibleByPlayer && Mathf.Abs(targetDelta) < _attackInfo.AttackRange;
            
            return canAttack;
        }

        private bool CanStopEscape()
        {
            return !CanEscape();
        }

        private bool IsOutOfBounds()
        {
            var target = _container.Navigation.Target;
            var targetDelta = _container.transform.position.x - target.transform.position.x;
            var escapeDirection = targetDelta / Mathf.Abs(targetDelta);

            var groundBounds = _container.GroundCollider.bounds;
            var containerBounds = _container.Collider.bounds;

            var stopEdge = _navigationInfo.StopEdgeValue;
            
            var isOutOfBounds = (escapeDirection > 0 && containerBounds.max.x + stopEdge > groundBounds.max.x) ||
                                (escapeDirection < 0 && containerBounds.min.x - stopEdge < groundBounds.min.x);

            return isOutOfBounds;
        }

        private bool CanStopAttack()
        {
            var canAttack = CanAttack();
            var isOutOfBounds = IsOutOfBounds();

            if (canAttack && isOutOfBounds)
            {
                return false;
            }
            
            return !canAttack && !_attack.IsAttacking;
        }
    }
}