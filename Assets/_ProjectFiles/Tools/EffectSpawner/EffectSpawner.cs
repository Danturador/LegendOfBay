using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
	public enum EffectType
	{
		Bleeding
	}

	[System.Serializable]
	public class Effect
	{
		public EffectType effectType;
		public GameObject prefab;
		public int poolSize;
	}

	public List<Effect> effects;
	private Dictionary<EffectType, Queue<GameObject>> effectPools;
	private static EffectSpawner _instance;

	public static EffectSpawner Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.LogError("EffectSpawner instance is null!");
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		effectPools = new Dictionary<EffectType, Queue<GameObject>>();
		foreach (var effect in effects)
		{
			Queue<GameObject> pool = new Queue<GameObject>();
			for (int i = 0; i < effect.poolSize; i++)
			{
				GameObject obj = Instantiate(effect.prefab, this.transform);
				obj.SetActive(false);
				pool.Enqueue(obj);
			}
			effectPools[effect.effectType] = pool;
		}
	}

	public void SpawnEffect(EffectType effectType, GameObject parent)
	{
		if (effectPools.TryGetValue(effectType, out Queue<GameObject> pool) && pool.Count > 0)
		{
			GameObject effectObject = pool.Dequeue();

			ActivateEffect(effectObject, parent.transform.position, effectType);
		}
		else
		{
			Effect effect = effects.Find(e => e.effectType == effectType);
			if (effect != null)
			{
				GameObject effectObject = Instantiate(effect.prefab, parent.transform);
				
				ActivateEffect(effectObject, parent.transform.position, effectType);

				pool.Enqueue(effectObject);
			}
			else
			{
				Debug.LogError($"Effect of type '{effectType}' not found!");
			}
		}
	}
	private void ActivateEffect(GameObject effectObject, Vector3 position, EffectType effectType)
	{
		effectObject.transform.position = position;
		effectObject.SetActive(true);
		StartCoroutine(DeactivateEffect(effectObject, effectType));
	}
	private IEnumerator DeactivateEffect(GameObject effectObject, EffectType effectType)
	{
		yield return new WaitForSeconds(2f);
		effectObject.SetActive(false);
		effectPools[effectType].Enqueue(effectObject);
	}
}