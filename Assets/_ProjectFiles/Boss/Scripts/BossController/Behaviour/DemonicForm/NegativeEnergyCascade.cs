using System;
using System.Collections;
using UnityEngine;
using static _ProjectFiles.SoundContainer.SoundType;

public class NegativeEnergyCascade : MonoBehaviour, IDemonicAttack
{
	[SerializeField] private GameObject projectionPrefab;
	[SerializeField] private GameObject beamPrefab;

	[SerializeField] private GameObject projection;
	[SerializeField] private GameObject beam;

	[SerializeField] private float projectionDuration;
	[SerializeField] private float beamDuration;
	[SerializeField] private float attackDelay;
	[SerializeField] private float rotationAngle;

	[SerializeField] private int countOfAttacks;

	private bool isDeinitialized = false;

	private void Initialize()
	{
		projection = Instantiate(projectionPrefab, transform.position, Quaternion.identity);
		beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);

		projectionDuration = 0.5f;
		beamDuration = 1f;
		attackDelay = 0.2f;
		rotationAngle = 15f;
		countOfAttacks = 4;
	}

	public void Deinitialize()
	{
		isDeinitialized = true;
		if (projection != null) Destroy(projection);
		if (beam != null) Destroy(beam);
	}

	public IEnumerator AttackPattern(Action<bool, bool> SetNegativeEnergyCascade)
	{
		Initialize();

		while (countOfAttacks > 0)
		{
			if (isDeinitialized || this == null)
			{
				yield break;
			}

			SetNegativeEnergyCascade(true, false);

			projection.SetActive(true);

			yield return new WaitForSeconds(projectionDuration);

			projection.SetActive(false);
			beam.SetActive(true);

			BossAudioInitializer.Instance.PlaySound(FireAttack);

			SetNegativeEnergyCascade(false, false);
			projection.SetActive(true);

			yield return new WaitForSeconds(beamDuration);

			if (isDeinitialized || this == null)
			{
				yield break;
			}

			beam.SetActive(false);
			RotateBeam(projection.transform, rotationAngle);
			RotateBeam(beam.transform, rotationAngle);

			projection.SetActive(false);
			yield return new WaitForSeconds(attackDelay);

			countOfAttacks--;
			SetNegativeEnergyCascade(false, false);
		}

		Deinitialize();
	}

	private void RotateBeam(Transform transform, float angle)
	{
		transform.Rotate(0, 0, angle);
	}
}