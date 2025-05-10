using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;

public class KirinPassiveState : IUpdateState
{
    private readonly EnemyContainer _container;

    public KirinPassiveState(EnemyContainer container)
    {
        _container = container;
    }

    public void Update()
    {
        var velocity = _container.Rigidbody.velocity;
        velocity.x = 0;
        _container.Rigidbody.velocity = velocity;
    }
}