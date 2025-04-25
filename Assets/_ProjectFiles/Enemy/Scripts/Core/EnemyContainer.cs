using _ProjectFiles.Enemy.Scripts._PLAYER_;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Kirin;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyContainer : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private new Collider2D collider;

        [SerializeField] private Animator animator;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private new EnemyRenderer renderer;

        [Header("Behaviour")] [SerializeField] private EnemyNavigation enemyNavigation;

        [SerializeField] private EnemyAttack enemyAttack;

        [Header("Data")] [SerializeField] private EnemyProfile profile;

        private Enemy _enemy;
        public EnemyNavigation Navigation => enemyNavigation;
        public EnemyAttack Attack => enemyAttack;
        public EnemyRenderer Renderer => renderer;
        public Animator Animator => animator;
        public Rigidbody2D Rigidbody => rigidbody;
        public EnemyProfile Profile => profile;
        public bool IsVisibleByPlayer { get; private set; }

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
                IsVisibleByPlayer = true;
                //_enemy.State.SetVisibility(true);
                Navigation.Target = zone.Player.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone)) IsVisibleByPlayer = false;
            //_enemy.State.SetVisibility(false);
        }

        private void Initialize()
        {
            _enemy = new Enemy(profile, this);

            switch (profile.EnemyInfo.Type)
            {
                case EnemyType.Hundun:
                {
                    var info = profile.NavigationInfo as HundunNavigationInfo;

                    enemyNavigation.Initialize(profile.NavigationInfo,
                        new HundunNavigation(rigidbody, info));

                    Attack.Initialize(this, new HundunAttack());
                    break;
                }

                case EnemyType.Kirin:
                {
                    enemyNavigation.Initialize(profile.NavigationInfo,
                        new KirinNavigation(this));

                    Attack.Initialize(this, new KirinAttack(this));
                    break;
                }
            }
        }
    }
}