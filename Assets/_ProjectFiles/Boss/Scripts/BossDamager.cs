using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamager : MonoBehaviour
{
	[SerializeField] private EffectSpawner effectSpawner;
	[SerializeField] private PlayerHealth playerHealth;
	[SerializeField] private int _damage = 5;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() == null) return;

		//collision.GetComponent<IDamageable>().TakeDamage(_damage);
		playerHealth.TakeDamage(_damage);
		EffectSpawner.Instance.SpawnEffect(EffectSpawner.EffectType.Bleeding, collision.gameObject);
		CameraShake();
	}
	private void CameraShake()
	{
		CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
	}
}
