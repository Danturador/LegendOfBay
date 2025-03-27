using _ProjectFiles.Enemy.Scripts._PLAYER_;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyContainer : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private new Collider2D collider;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private new EnemyRenderer renderer;

        [Header("Behaviour")] 
        [SerializeField] private EnemyNavigation enemyNavigation;

        [Header("Profile")]
        [SerializeField] private EnemyProfile profile;

        private Enemy _enemy;
        public EnemyNavigation Navigation => enemyNavigation;
        public EnemyRenderer Renderer => renderer;
        public bool IsVisibleByPlayer { get; private set; }
        public EnemyDetectionZone Target { get; private set; }

        private void Start()
        {
            Initialize();
            Debug.Log(collider.bounds);
        }

        private void Update()
        {
            _enemy.State.Update();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone))
            {
                IsVisibleByPlayer = true; 
                Target = zone;
                Navigation.Target = Target.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone))
            {
                Target = null;
                IsVisibleByPlayer = false;
                Navigation.Target = null;
            };
        }

        private void Initialize()
        {
            _enemy = new Enemy(profile, this);
            INavigationExecutable executable = null;

            switch (profile.Type)
            {
                case EnemyType.Hundun:
                {
                    executable = new HundunNavigationExecutable(this, profile.NavigationInfo);
                    break;
                }
                
                case EnemyType.Shishi:
                {
                    executable = new ShishiNavigationExecutable(this, profile.NavigationInfo);
                    break;
                }
            }
            
            enemyNavigation.Initialize(profile.NavigationInfo,
                executable);
        }
    }
}