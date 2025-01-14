using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private EnemyInfo enemyInfo;
    [SerializeField] private EnemyAttackInfo attackInfo;
    [SerializeField] private EnemyNavigationInfo navigationInfo;
    private Enemy _enemy;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        Initialize();
    }

    private void Update()
    {
        _enemy.State.IsVisibleByPlayer = _renderer.isVisible;
        _enemy.State.Update();
    }

    public bool IsVisi
    
    public void Initialize( /*EnemyInfo enemyInfo, EnemyAttackInfo attackInfo, EnemyNavigationInfo navigationInfo*/)
    {
        _enemy = new Enemy(enemyInfo, attackInfo, navigationInfo);
    }
}