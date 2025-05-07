using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeOfNeedles : MonoBehaviour, IDemonicAttack
{
	[SerializeField] private GameObject spikePrefab;
	[SerializeField] private List<GameObject> spikes = new List<GameObject>();
	[SerializeField] private float spikeSpacing;
	[SerializeField] private int totalSpikes;
	[SerializeField] private float attackDelay;
	[SerializeField] private float animationDelay;
	[SerializeField] private float spikeFallSpeed;
	[SerializeField] private int[,] skipSpikes;
	[SerializeField] private int attackIndex;
	[SerializeField] private float startFallingPoint;
	[SerializeField] private float endFallingPoint;
	[SerializeField] private float delayBeforeFalling;
	private bool isDead;

	private void Initialize()
	{
		skipSpikes = new int[4, 2] {
			{ 2, 5 },
			{ 5, 8 },
			{ 10, 12 },
			{ 6, 14 }
		};
		attackIndex = 0;
		isDead = false;
	}

	public void Deinitialize()
	{
		isDead = true;

		List<GameObject> spikesToRemove = new List<GameObject>(spikes);

		foreach (var spike in spikesToRemove)
		{
			RemoveSpike(spike);
		}

		spikes.Clear();
	}

	private void RemoveSpike(GameObject spike)
	{
		if (spike != null)
		{
			spikes.Remove(spike);
			Destroy(spike);
		}
	}

	public IEnumerator AttackPattern(Action<bool> setCascadeOfNeedles)
	{
		Initialize();

		while (attackIndex < skipSpikes.GetLength(0))
		{
			setCascadeOfNeedles(true);

			SpawnSpikes();
			if (attackIndex < skipSpikes.GetLength(0))
			{
				yield return new WaitForSeconds(attackDelay);
			}
			setCascadeOfNeedles(false);
		}
	}

	private void SpawnSpikes()
	{
		float halfWidth = (totalSpikes - 1) * spikeSpacing / 2;

		for (int i = 0; i < totalSpikes; i++)
		{
			if (i != skipSpikes[attackIndex, 0] && i != skipSpikes[attackIndex, 1])
			{
				Vector3 spawnPosition = transform.position + new Vector3(i * spikeSpacing - halfWidth, startFallingPoint, 0);
				if (isDead) break;
				SpawnSpike(spawnPosition);
			}
		}
		attackIndex++;
	}

	private void SpawnSpike(Vector3 position)
	{
		GameObject spike = Instantiate(spikePrefab, position, Quaternion.identity, this.gameObject.transform);
		spikes.Add(spike);
		StartCoroutine(Fall(spike));
	}

	private IEnumerator Fall(GameObject spike)
	{
		yield return new WaitForSeconds(delayBeforeFalling);

		while (spike != null && spike.transform.localPosition.y > endFallingPoint)
		{
			spike.transform.position += Vector3.down * spikeFallSpeed * Time.deltaTime;
			yield return null;
		}
		if (spike != null)
		{
			RemoveSpike(spike);
		}
	}
}