using _ProjectFiles.Enemy.Scripts.Core;

public class ActiveState : IEnterState, IUpdateState, IExitState
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

    public void Update()
    {
    }
}