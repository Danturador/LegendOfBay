using _ProjectFiles.Enemy.Scripts.Core.StateMachines;
using Unity.VisualScripting.Dependencies.NCalc;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class Enemy
    {
        private readonly EnemyProfile _profile;
        private readonly EnemyContainer _container;

        public Enemy(EnemyProfile profile, EnemyContainer container)
        {
            _profile = profile;
            _container = container;

            switch (profile.Type)
            {
                case EnemyType.Hundun:
                {
                    State = new HundunStateMachine(_container, _profile);
                    break;
                }
                
                case EnemyType.Shishi:
                {
                    State = new ShishiStateMachine(_container, _profile);
                    break;
                }
            }
        }

        public StateMachine State { get; }
    }
}