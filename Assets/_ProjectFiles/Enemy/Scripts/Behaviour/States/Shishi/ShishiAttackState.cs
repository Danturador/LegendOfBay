using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Shishi;
using Unity.VisualScripting;
using UnityEngine;

public class ShishiAttackState : IEnterState, IUpdateState, IExitState
{
    private EnemyContainer _container;
    
    public ShishiAttackState(EnemyContainer container)
    {
        _container = container;
    }
    
    public void Enter()
    {
        Debug.Log("AttackState enter");
        _container.Renderer.SetSAttackStateEnabled(true);
    }

    public void Exit()
    {
        Debug.Log("AttackState exit");
        _container.Renderer.SetSAttackStateEnabled(false);
    }

    public void Update()
    {
        Debug.Log("AttackState update");
    }

    public bool CanAttack()
    {
        return _container.IsVisibleByPlayer;
    }

    public bool CannotAttack()
    {
        return !_container.IsVisibleByPlayer;
    }
}