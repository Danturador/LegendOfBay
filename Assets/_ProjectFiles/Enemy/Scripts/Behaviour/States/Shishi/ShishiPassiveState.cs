using _ProjectFiles.Enemy.Scripts.Core;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi
{
    public class ShishiPassiveState : IUpdateState
    {
        private readonly EnemyContainer _container;

        public ShishiPassiveState(EnemyContainer container)
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
}