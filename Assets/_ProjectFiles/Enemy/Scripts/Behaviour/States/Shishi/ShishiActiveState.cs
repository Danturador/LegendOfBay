using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Shishi;
using UnityEngine;

public class ShishiActiveState : IEnterState, IUpdateState, IExitState
{
    private EnemyContainer _container;
    private ShishiNavigationInfo _navigationInfo;
    
    public ShishiActiveState(EnemyContainer container, ShishiNavigationInfo navigationInfo)
    {
        _container = container;
        _navigationInfo = navigationInfo;
    }
    
    public void Enter()
    {
        _container.Navigation.Execute();
        _container.Renderer.SetActiveStateEnabled(true);
        Debug.Log("Shishi Active");
    }

    public void Exit()
    {
        _container.Navigation.Stop();
        _container.Renderer.SetActiveStateEnabled(false);
        Debug.Log("Shishi actrive exit");
    }

    public void Update()
    {
        Debug.Log("Shishi Active update");
    }

    public bool IsInEscapeRange()
    {
        bool canEscape = CanEscape();
        return canEscape;
    }
    
    public bool IsOutOfEscapeRange()
    {
        bool canEscape = CanEscape();
        return !canEscape;
    }

    private bool CanEscape()
    {
        if (!_container.IsVisibleByPlayer) return false;
        
        var target = _container.Target;
        if (!target) return false;
        
        var distanceToTarget = Vector2.Distance(_container.transform.position, target.transform.position);
        var canEscape = distanceToTarget < _navigationInfo.EscapeRange;
        return canEscape;
    }
}