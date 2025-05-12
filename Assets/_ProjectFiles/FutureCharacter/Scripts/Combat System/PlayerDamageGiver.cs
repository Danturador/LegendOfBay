using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDamageGiver : MonoBehaviour
{
    [SerializeField] private GameObject[] _hitEffects;
    private int _currentEffectIndex = 0;
    private float _attackRadius;
    private GameObject _damageGiverGameObject;
    private LayerMask _damageLayer;
    private PlayerAudioInitializer _audioInitializer;
    [SerializeField]private PlayerJump _playerJump;
    private void Awake()
    {
        _attackRadius = 2f;
        _damageLayer = 1 << 9;
        _damageGiverGameObject = GameObject.Find("PlayerDamageGiver");
    }
    private void Start()
    {
        _audioInitializer = GetComponent<PlayerAudioInitializer>();
        _playerJump = GetComponentInParent<PlayerJump>();

    }

    public void AttackSlash(int numberEffect)
    {
        CreateHitEffect(_damageGiverGameObject.transform, numberEffect);
    }
    public void GiveDamage(int _damage)
    {
        var colliders = Physics2D.OverlapCircleAll(_damageGiverGameObject.transform.position, _attackRadius); //_damageLayer );
        var foundItem = colliders.Where(t => t.GetComponent<IDamageable>() != null).ToList();
        if (foundItem.Count != 0)
        {
            Debug.Log("foundMonster");
            foreach (var item in foundItem)
            {
                item.GetComponent<IDamageable>().TakeDamage(_damage);
                CreateHitEffect(item.transform,0);
                _audioInitializer.PlayerHitEnemySound(1);
                CameraShake();
            }
        }
    }
    private void CreateHitEffect(Transform itemPosition,int numberEffect)
    {
        if(_hitEffects != null)
        {
            Vector2 hitPosition = itemPosition.position;
            GameObject effect = Instantiate(_hitEffects[numberEffect], hitPosition, Quaternion.identity);

            ParticleSystemRenderer psRenderer = effect.GetComponent<ParticleSystemRenderer>();
            if (psRenderer != null)
            {
                psRenderer.sortingOrder = 9;
            }
            Destroy(effect, 2f);
            //_currentEffectIndex = (_currentEffectIndex + 1) % _hitEffects.Length;
        }
    }

    private void CameraShake()
    {
        CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
    }

    public void StopGravity(int number)
    {
        if(number == 1)
        {
            _playerJump.StopGravity(true);
        }
        else
        {
            _playerJump.StopGravity(false);
        }
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.green;
       // Gizmos.DrawSphere(new Vector3(_damageGiverGameObject.transform.position.x, _damageGiverGameObject.transform.position.y, 0), _attackRadius);
    }
}
