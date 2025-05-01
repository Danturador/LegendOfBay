using _ProjectFiles.Enemy.Scripts.Core;

public class HundunActiveState : IEnterState, IExitState
{
    public EnemyContainer Container { get; set; }

    public void Enter()
    {
        Container.Navigation.Execute();
    }

    public void Exit()
    {
        Container.Navigation.Stop();
    }
}