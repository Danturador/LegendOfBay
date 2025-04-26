using _ProjectFiles.Enemy.Scripts.Core;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi
{
    public class ShishiActiveState : IEnterState, IExitState
    {
        public EnemyContainer _container;

        public ShishiActiveState(EnemyContainer container)
        {
            _container = container;
        }

        public void Enter()
        {
            _container.Animator.SetBool("active", true);
            _container.Navigation.Execute();
        }

        public void Exit()
        {
            _container.Navigation.Stop();
            _container.Animator.SetBool("active", false);
        }
    }
}