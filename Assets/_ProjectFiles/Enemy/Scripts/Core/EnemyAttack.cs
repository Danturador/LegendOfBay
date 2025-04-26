using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using UnityEditorInternal;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hitEffects;
        private IAttackExecutable _attack;
        private EnemyContainer _container;
        private int _currentEffectIndex;
        private EnemyAttackInfo _info;
        public IAttackExecutable AttackExecutable => _attack;

        public void Initialize(EnemyContainer container, IAttackExecutable executable)
        {
            _info = container.Profile.AttackInfo;
            _container = container;
            _attack = executable;
        }

        public void Attack()
        {
            _container.Navigation.Target.GetComponentInChildren<IDamageable>().TakeDamage(_info.Damage);
            CreateHitEffect(_container.Navigation.Target.transform);
            CameraShake();
        }

        public void Execute()
        {
            StartCoroutine(_attack.Execute(_container.Navigation.Target));
        }

        public void Stop()
        {
            _attack.Stop();
        }

        private void CreateHitEffect(Transform itemPosition)
        {
            if (_hitEffects != null)
            {
                Vector2 hitPosition = itemPosition.position;
                var effect = Instantiate(_hitEffects[_currentEffectIndex], hitPosition, Quaternion.identity);

                var psRenderer = effect.GetComponent<ParticleSystemRenderer>();
                if (psRenderer != null) psRenderer.sortingOrder = 9;
                Destroy(effect, 2f);
                _currentEffectIndex = (_currentEffectIndex + 1) % _hitEffects.Length;
            }
        }

        private void CameraShake()
        {
            CinemachineShake.Instance.ShakeCamera(7f, 0.1f);
        }
    }
}