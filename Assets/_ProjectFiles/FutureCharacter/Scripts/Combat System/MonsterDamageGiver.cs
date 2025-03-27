using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDamageGiver : MonoBehaviour
{
    [SerializeField] private GameObject[] _hitEffects;
    [SerializeField] private int _damage = 5;
    private int _currentEffectIndex = 0;
    private LayerMask _damageLayer;
    private bool _damageDone;
    private void Awake()
    {
        _damageLayer = 1 << 7;
    }
    private void CreateHitEffect(Transform itemPosition)
    {
        if (_hitEffects != null)
        {
            Vector2 hitPosition = itemPosition.position;
            GameObject effect = Instantiate(_hitEffects[_currentEffectIndex], hitPosition, Quaternion.identity);

            ParticleSystemRenderer psRenderer = effect.GetComponent<ParticleSystemRenderer>();
            if (psRenderer != null)
            {
                psRenderer.sortingOrder = 9;
            }
            Destroy(effect, 2f);
            _currentEffectIndex = (_currentEffectIndex + 1) % _hitEffects.Length;
        }
    }

    private void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(7f, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _damageLayer) != 0 && _damageDone == false)
        {
            _damageDone = true;
            Debug.Log(collision.name);
            collision.GetComponentInChildren<IDamageable>().TakeDamage(_damage);
            CreateHitEffect(collision.transform);
            CameraShake();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _damageDone = false;
    }
}
