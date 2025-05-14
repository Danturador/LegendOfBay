using _ProjectFiles.Enemy.Scripts.Core;

public class HundunAttackState : IEnterState, IExitState
{
    private readonly EnemyContainer _container;

    public HundunAttackState(EnemyContainer container)
    {
        _container = container;
    }

    public void Enter()
    {
        _container.Attack.Execute();
    }

    public void Exit()
    {
        _container.Attack.Stop();
    }
}