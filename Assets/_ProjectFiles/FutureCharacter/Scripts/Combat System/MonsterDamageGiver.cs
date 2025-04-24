using System;
using System.Threading.Tasks;
using UnityEngine;

public class MonsterDamageGiver : MonoBehaviour
{
    [SerializeField] private GameObject[] _hitEffects;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float attackStartDelay;
    [SerializeField] private float attackDelay;
    [SerializeField] private Animator _animator;
    private bool _attackStarted;
    private int _currentEffectIndex;
    private bool _damageDone;
    private LayerMask _damageLayer;

    private void Awake()
    {
        _damageLayer = 1 << 7;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PerformAttack(collision);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _attackStarted = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PerformAttack(collision);
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

    private void PerformAttack(Collider2D collision)
    {
        if (_attackStarted) return;

        if (((1 << collision.gameObject.layer) & _damageLayer) != 0 && _damageDone == false)
        {
            _attackStarted = true;
            Attack(collision);
        }
    }

    private async Task Attack(Collider2D collision)
    {
        Debug.Log("attack");
        ReloadAttack();
        _animator.SetTrigger("attack");
        await Task.Delay(TimeSpan.FromSeconds(attackStartDelay));

        if (!_attackStarted)
        {
            _animator.SetTrigger("idle");
            return;
        }

        _damageDone = true;
        Debug.Log(collision.name);
        collision.GetComponentInChildren<IDamageable>().TakeDamage(_damage);
        CreateHitEffect(collision.transform);
        CameraShake();
        _attackStarted = false;
    }

    private async Task ReloadAttack()
    {
        await Task.Delay(TimeSpan.FromSeconds(attackDelay));
        _damageDone = false;
        Debug.Log("attack reloaded");
    }
}