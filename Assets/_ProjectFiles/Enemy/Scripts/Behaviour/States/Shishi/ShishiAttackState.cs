using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi
{
    public class ShishiAttackState : IEnterState, IExitState
    {
        private readonly EnemyContainer _container;

        public ShishiAttackState(EnemyContainer container)
        {
            _container = container;
        }

        public void Enter()
        {
            var target = _container.Navigation.Target;
            var targetDelta = _container.transform.position.x - target.transform.position.x;
            var lookDirection = -(int)(targetDelta / Mathf.Abs(targetDelta));

            _container.Renderer.CurrentScale = lookDirection;
            _container.Attack.Execute();
        }

        public void Exit()
        {
            _container.Attack.Stop();
        }
    }
}