using _ProjectFiles.Enemy.Scripts._PLAYER_;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Kirin;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun;
using UnityEngine;
using ShishiAttack = _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi.ShishiAttack;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyContainer : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private new Collider2D collider;

        [SerializeField] private Animator animator;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private new EnemyRenderer renderer;
        [SerializeField] private MonsterHealth monsterHealth;
        [SerializeField] private new EnemyAudio audio;

        [Header("Behaviour")] [SerializeField] private EnemyNavigation enemyNavigation;

        [SerializeField] private EnemyAttack enemyAttack;

        [Header("Data")] [SerializeField] private EnemyProfile profile;

        private Enemy _enemy;
        private EnemyDetectionZone _targetZone;
        public EnemyNavigation Navigation => enemyNavigation;
        public EnemyAttack Attack => enemyAttack;
        public MonsterHealth Health => monsterHealth;
        public EnemyRenderer Renderer => renderer;
        public EnemyAudio Audio => audio;
        public Animator Animator => animator;
        public Rigidbody2D Rigidbody => rigidbody;
        public EnemyProfile Profile => profile;
        public Collider2D Collider => collider;
        public Collider2D GroundCollider { get; private set; }

        public bool IsVisibleByPlayer { get; private set; }
        public bool IsInitialized { get; private set; }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            _enemy.State.Update();

            if (_targetZone != null)
            {
                var zoneCollider = _targetZone.Collider;
                var largerBounds = zoneCollider.bounds;
                var smallerBounds = collider.bounds;

                var isFit = largerBounds.Contains(smallerBounds.min) && largerBounds.Contains(smallerBounds.max);

                IsVisibleByPlayer = isFit;
                Navigation.Target = _targetZone.Player;
            }
            else
            {
                IsVisibleByPlayer = false;
                Navigation.Target = null;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            GroundCollider = other.collider;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone)) _targetZone = zone;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyDetectionZone zone)) _targetZone = null;
        }

        private void Initialize()
        {
            switch (profile.EnemyInfo.Type)
            {
                case EnemyType.Hundun:
                {
                    var info = profile.NavigationInfo as HundunNavigationInfo;

                    enemyNavigation.Initialize(profile.NavigationInfo,
                        new HundunNavigation(this, info));

                    Attack.Initialize(this, new HundunAttack(this));
                    break;
                }

                case EnemyType.Kirin:
                {
                    enemyNavigation.Initialize(profile.NavigationInfo,
                        new KirinNavigation(this));

                    Attack.Initialize(this, new KirinAttack(this));
                    break;
                }

                case EnemyType.Shishi:
                {
                    enemyNavigation.Initialize(profile.NavigationInfo,
                        new ShishiNavigation(this));

                    Attack.Initialize(this, new ShishiAttack(this));
                    break;
                }
            }

            _enemy = new Enemy(profile, this);
            IsInitialized = true;
        }
    }
}