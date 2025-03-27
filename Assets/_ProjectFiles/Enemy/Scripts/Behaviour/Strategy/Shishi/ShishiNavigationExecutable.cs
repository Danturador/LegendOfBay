using System.Collections;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Shishi;
using UnityEngine;

public class ShishiNavigationExecutable : INavigationExecutable
{
    private Rigidbody2D _rigidbody;
    private ShishiNavigationInfo _navigationInfo;
    private bool _isActive;

    public ShishiNavigationExecutable(EnemyContainer container, EnemyNavigationInfo info)
    {
        _rigidbody = container.GetComponent<Rigidbody2D>();
        _navigationInfo = info as ShishiNavigationInfo;
    }

    bool INavigationExecutable.IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    public IEnumerator Execute(Transform target)
    {
        _isActive = true;
        Vector2 moveDirection = (_rigidbody.transform.position - target.position).normalized;
        moveDirection.y = 0;
        _rigidbody.velocity = _navigationInfo.MoveSpeed * moveDirection;
        
        
        while (true)
        {
            if (!_isActive) yield break;
            
            moveDirection = (_rigidbody.transform.position - target.position).normalized;
            moveDirection.y = 0;
            _rigidbody.velocity = _navigationInfo.MoveSpeed * moveDirection;
            yield return null;
        }
        yield break;
    }

    public void Stop()
    {
        _isActive = false;
        _rigidbody.velocity = Vector2.zero;
    }
}
