using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CascadeOfNeedles : MonoBehaviour, IDemonicAttack
{
	[SerializeField] private GameObject spikePrefab;
	[SerializeField] private float spikeSpacing;
	[SerializeField] private int totalSpikes;
	[SerializeField] private float attackDelay;
	[SerializeField] private float spikeFallSpeed;
	[SerializeField] private int[] skipSpikes;
	[SerializeField] private int attackIndex;
	[SerializeField] private float startFallingPoint;
	[SerializeField] private float endFallingPoint;

	private void Initialize()
	{
		skipSpikes = new int[4] { 2, 5, 10, 14 };
		attackIndex = 0;
	}

	public IEnumerator AttackPattern()
	{
		Initialize();

		while (attackIndex < skipSpikes.Length)
		{
			SpawnSpikes();
			yield return new WaitForSeconds(attackDelay);
		}
	}

	private void SpawnSpikes()
	{
		float halfWidth = (totalSpikes - 1) * spikeSpacing / 2;

		for (int i = 0; i < totalSpikes; i++)
		{
			if (i != skipSpikes[attackIndex])
			{
				Vector3 spawnPosition = transform.position + new Vector3(i * spikeSpacing - halfWidth, startFallingPoint, 0);
				SpawnSpike(spawnPosition);
			}
		}
		attackIndex++;
	}

	private void SpawnSpike(Vector3 position)
	{
		GameObject spike = Instantiate(spikePrefab, position, Quaternion.identity);
		StartCoroutine(Fall(spike));
	}

	private IEnumerator Fall(GameObject spike)
	{
		while (spike.transform.position.y > endFallingPoint)
		{
			spike.transform.position += Vector3.down * spikeFallSpeed * Time.deltaTime;
			yield return null;
		}
		Destroy(spike);
	}
}