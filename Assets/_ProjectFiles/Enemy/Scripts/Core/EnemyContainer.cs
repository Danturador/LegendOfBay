using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private EnemyInfo enemyInfo;
    [SerializeField] private EnemyAttackInfo attackInfo;
    [SerializeField] private EnemyNavigationInfo navigationInfo;
    [SerializeField] private new Collider2D collider;
    private Enemy _enemy;
    private RenderVisibility _visibility;

    private void Start()
    {
        _visibility = new RenderVisibility(collider);
        Initialize();
    }

    private void Update()
    {
        _enemy.State.IsVisibleByPlayer = _visibility.IsVisible;
        _enemy.State.Update();
    }
    
    public void Initialize()
    {
        _enemy = new Enemy(enemyInfo, attackInfo, navigationInfo);
    }
}