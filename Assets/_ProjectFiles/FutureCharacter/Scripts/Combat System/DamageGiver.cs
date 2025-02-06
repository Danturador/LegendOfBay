using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask = 9;
    [SerializeField] private AudioSource _audioSource;
    private GameObject _damageGiver;
    private ParticleSystem _damageParticleSystem;

    private void Start()
    {
        _damageGiver = GameObject.Find("PlayerDamageGiver");
        _damageParticleSystem = GameObject.Find("DamageParicleSystem").GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SmallAttack(int damage)
    {
        _audioSource.Play();
        Debug.Log(damage);
        var colliders = Physics2D.OverlapCircleAll(_damageGiver.transform.position, 2f, _layerMask);
        var foundItem = colliders.Where(t => t.GetComponent<MonsterHealth>() != null).ToList();
        if (foundItem.Count != 0) 
        {
            Debug.Log("Found monster");
            foreach (var t in foundItem)
            {
                _damageParticleSystem.startColor = Color.cyan;
                _damageParticleSystem.transform.position = t.transform.position;
                _damageParticleSystem.Play();
                t.GetComponent<MonsterHealth>().TakeDamage(damage);
            }
        }
       
    }

    public void MediumAttack(int damage)
    {
        var colliders = Physics2D.OverlapCircleAll(_damageGiver.transform.position, 3f, _layerMask);
        var foundItem = colliders.Where(t => t.GetComponent<MonsterHealth>() != null).ToList();
        if (foundItem.Count != 0)
        {
            Debug.Log("Found monster");
            foreach (var t in foundItem)
            {
                _damageParticleSystem.startColor = Color.red;
                _damageParticleSystem.transform.position = t.transform.position;
                _damageParticleSystem.Play();
                t.GetComponent<MonsterHealth>().TakeDamage(damage);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawSphere(_damageGiver.transform.position, 1f);
    }
}
