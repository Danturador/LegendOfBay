using _ProjectFiles.Enemy.Scripts.Core;

public class HundunActiveState : IEnterState, IUpdateState, IExitState
{
    private EnemyContainer _container;

    public HundunActiveState(EnemyContainer container)
    {
        _container = container;
    }

    public void Enter()
    {
        _container.Navigation.Execute();
    }

    public void Exit()
    {
        _container.Navigation.Stop();
    }

    public void Update()
    {
    }

    public bool IsInPlayerRange()
    {
        return _container.IsVisibleByPlayer;
    }
    
    public bool IsOutOfPlayerRange()
    {
        return !_container.IsVisibleByPlayer;
    }
}