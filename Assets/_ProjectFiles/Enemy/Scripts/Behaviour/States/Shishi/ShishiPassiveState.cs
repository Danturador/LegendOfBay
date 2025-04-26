using _ProjectFiles.Enemy.Scripts.Core;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi
{
    public class ShishiPassiveState : IEnterState, IExitState
    {
        private EnemyContainer _container;

        public ShishiPassiveState(EnemyContainer container)
        {
            _container = container;
        }

        public void Enter()
        {
            //_container.Animator.SetBool("");
        }

        public void Exit()
        {
        }
    }
}