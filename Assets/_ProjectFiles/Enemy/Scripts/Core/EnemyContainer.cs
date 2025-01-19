using _ProjectFiles.Enemy.Scripts._PLAYER_;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyContainer : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private new Collider2D collider;

        [SerializeField] private new Rigidbody2D rigidbody;

        [Header("Behaviour")] [SerializeField] private EnemyNavigation enemyNavigation;

        [SerializeField] private AnimationCurve speedCurve;

        [Header("Data")] [SerializeField] private EnemyInfoContainer infoContainer;

        private Enemy _enemy;
        public EnemyNavigation Navigation => enemyNavigation;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _enemy.State.Update();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone))
            {
                _enemy.State.SetVisibility(true);
                Navigation.Target = zone.Player.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone)) _enemy.State.SetVisibility(false);
        }

        private void Initialize()
        {
            _enemy = new Enemy(infoContainer, this);

            enemyNavigation.Initialize(infoContainer.NavigationInfo,
                new HundunNavigation(rigidbody, infoContainer.NavigationInfo, speedCurve));
        }
    }
}