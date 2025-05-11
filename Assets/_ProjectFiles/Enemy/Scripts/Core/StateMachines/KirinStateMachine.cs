using System.Linq;
using _ProjectFiles.Enemy.Scripts.Behaviour.States.Kirin;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Kirin;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class KirinStateMachine : StateMachine
    {
        private readonly KirinAttackInfo _attackInfo;
        private readonly KirinNavigationInfo _navigationInfo;
        private readonly KirinActiveState _hundunActiveState;
        private bool _isVisibleByPlayer;

        public KirinStateMachine(EnemyProfile profile, EnemyContainer container) : base(container)
        {
            var activeState = (KirinActiveState)_states.ToList().First(x => x.GetType() == typeof(KirinActiveState));
            activeState.Container = _container;
            _attackInfo = _container.Profile.AttackInfo as KirinAttackInfo;
            _navigationInfo = _container.Profile.NavigationInfo as KirinNavigationInfo;
        }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var transitions = new[]
            {
                new Transition(typeof(KirinPassiveState), typeof(KirinActiveState), CanChase),
                new Transition(typeof(KirinPassiveState), typeof(KirinAttackState), CanAttack),
                new Transition(typeof(KirinActiveState), typeof(KirinPassiveState), CanStopChase),
                new Transition(typeof(KirinActiveState), typeof(KirinAttackState), CanAttack),
                new Transition(typeof(KirinAttackState), typeof(KirinActiveState), CanStopAttack)
            };

            var states = new IState[]
                { new KirinPassiveState(_container), new KirinActiveState(), new KirinAttackState(_container) };

            return (states, transitions);
        }

        private bool CanAttack()
        {
            if (_container.Navigation.Target == null) return false;
            
            var navigationTarget = _container.Navigation.Target;
            
            var targetDistance =
                Mathf.Abs(navigationTarget.transform.position.x - _container.transform.position.x);

            var isInPreparationState = ((KirinNavigation)_container.Navigation.NavigationExecutable).IsPreparing;

            var areIntersecting = _container.Collider.bounds.max.y >= navigationTarget.Collider.bounds.min.y;
            
            return targetDistance < _attackInfo.AttackRange && !isInPreparationState && areIntersecting;
        }
        
        private bool IsOutOfBounds()
        {
            var target = _container.Navigation.Target;
            if (target == null) return true;
            
            var targetDelta = _container.transform.position.x - target.transform.position.x;
            var chaseDirection = -targetDelta / Mathf.Abs(targetDelta);

            var groundBounds = _container.GroundCollider.bounds;
            var containerBounds = _container.Collider.bounds;

            var stopEdge = _navigationInfo.StopEdgeValue;
            
            var isOutOfBounds = (chaseDirection > 0 && containerBounds.max.x + stopEdge > groundBounds.max.x) ||
                                (chaseDirection < 0 && containerBounds.min.x - stopEdge < groundBounds.min.x);

            return isOutOfBounds;
        }

        private bool CanChase()
        {
            var isOutOfBounds = IsOutOfBounds();
            return _container.IsVisibleByPlayer && !isOutOfBounds;
        }

        private bool CanStopAttack()
        {
            var canAttack = CanAttack();
            var isOutOfBounds = IsOutOfBounds();

            if (isOutOfBounds)
            {
                return true;
            } 
            
            return !canAttack;
        }

        private bool CanStopChase()
        {
            var canChase = CanChase();
            var isOutOfBounds = IsOutOfBounds();

            if (isOutOfBounds)
            {
                return true;
            }
            
            return !canChase;
        }
    }
}