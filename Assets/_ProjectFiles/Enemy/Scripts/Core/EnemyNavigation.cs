using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyNavigation : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        private EnemyNavigationInfo _info;
        private INavigationExecutable _navigationExecutable;
        public Transform Target { get; set; }

        public void Initialize(EnemyNavigationInfo info, INavigationExecutable executable)
        {
            _info = info;
            _navigationExecutable = executable;
        }

        public void Execute()
        {
            StartCoroutine( _navigationExecutable.Execute(Target));
        }

        public void Stop()
        {
            _navigationExecutable.Stop();
        }
    }
}