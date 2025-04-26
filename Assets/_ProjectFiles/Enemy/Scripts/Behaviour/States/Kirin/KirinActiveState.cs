using _ProjectFiles.Enemy.Scripts.Core;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Kirin
{
    public class KirinActiveState : IEnterState, IExitState
    {
        public EnemyContainer Container { get; set; }

        public void Enter()
        {
            Container.Navigation.Execute();
        }

        public void Exit()
        {
            Container.Navigation.Stop();
            Container.Animator.SetTrigger("idle");
        }
    }
}