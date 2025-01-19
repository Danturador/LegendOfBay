using _ProjectFiles.Enemy.Scripts.Core;

namespace _ProjectFiles.Enemy.Scripts.Behaviour
{
    public class EnemyState
    {
        protected readonly EnemyContainer EnemyContainer;

        protected EnemyState(EnemyContainer container)
        {
            EnemyContainer = container;
        }
    }
}