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
	[SerializeField] private float cascadeDelay;
	private bool isDead;

	private enum AttackType { Line, Cascade }

	[SerializeField] private List<AttackType> attackSequence = new List<AttackType> { AttackType.Line, AttackType.Cascade, AttackType.Line, AttackType.Cascade };
	private int sequenceIndex = 0;


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
		sequenceIndex = 0;
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

		while (sequenceIndex < attackSequence.Count)
		{
			setCascadeOfNeedles(true);

			if (attackSequence[sequenceIndex] == AttackType.Line)
			{
				SpawnLineAttack();
			}
			else if (attackSequence[sequenceIndex] == AttackType.Cascade)
			{
				yield return StartCoroutine(SpawnCascadeAttack());
			}

			yield return new WaitForSeconds(attackDelay);
			setCascadeOfNeedles(false);
			sequenceIndex++;
		}
	}

	private void SpawnLineAttack()
	{
		float halfWidth = (totalSpikes - 1) * spikeSpacing / 2;

		for (int i = 0; i < totalSpikes; i++)
		{
			if (i != skipSpikes[attackIndex, 0] && i != skipSpikes[attackIndex, 1])
			{
				Vector3 spawnPosition = transform.position + new Vector3(i * spikeSpacing - halfWidth, startFallingPoint, 0);
				if (isDead) break;
				GameObject spike = SpawnSpike(spawnPosition);
				StartCoroutine(Fall(spike));
			}
		}
		attackIndex++;
		if (attackIndex >= skipSpikes.GetLength(0))
		{
			attackIndex = 0;
		}
	}

	private IEnumerator SpawnCascadeAttack()
	{
		float halfWidth = (totalSpikes - 1) * spikeSpacing / 2;
		List<GameObject> cascadeSpikes = new List<GameObject>();

		for (int i = 0; i < totalSpikes; i++)
		{
			Vector3 spawnPosition = transform.position + new Vector3(i * spikeSpacing - halfWidth, startFallingPoint, 0);
			if (isDead) break;
			GameObject spike = SpawnSpike(spawnPosition);
			cascadeSpikes.Add(spike);
		}

		foreach (GameObject spike in cascadeSpikes)
		{
			if (spike != null)
			{
				StartCoroutine(Fall(spike));
				yield return new WaitForSeconds(cascadeDelay);
			}
		}
	}

	private GameObject SpawnSpike(Vector3 position)
	{
		GameObject spike = Instantiate(spikePrefab, position, Quaternion.identity, this.gameObject.transform);
		spikes.Add(spike);
		return spike;
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