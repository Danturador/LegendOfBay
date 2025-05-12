using System;
using System.Collections;
using UnityEngine;
using static _ProjectFiles.SoundContainer.SoundType;

public class NegativeEnergyCascade : MonoBehaviour, IDemonicAttack
{
	[SerializeField] private GameObject beamParentPrefab;
	[SerializeField] private GameObject projectionPrefab;
	[SerializeField] private GameObject beamPrefab;

	[SerializeField] private GameObject beamParent;
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
		beamParent = Instantiate(beamParentPrefab, transform.position, Quaternion.identity);
		beamParent.transform.SetParent(this.transform);

		projectionDuration = 0.5f;
		beamDuration = 1f;
		attackDelay = 0.2f;
		rotationAngle = 15f;
		countOfAttacks = 4;
	}
	private void SpawnBeam()
	{
		projection = Instantiate(projectionPrefab, beamParent.transform);
		beam = Instantiate(beamPrefab, beamParent.transform);

		beam.SetActive(false);
		projection.transform.SetParent(beamParent.transform);
		beam.transform.SetParent(beamParent.transform);

		//projection.transform.localPosition = Vector3.zero;
		//projection.transform.localRotation = Quaternion.identity;
		//beam.transform.localPosition = Vector3.zero;
		//beam.transform.localRotation = Quaternion.identity;
	}
	public void DeinitializeComplitely()
	{
		isDeinitialized = true;

		if (beamParent != null) Destroy(beamParent);
		Deinitialize();
	}
	private void Deinitialize()
	{
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

			SpawnBeam();

			SetNegativeEnergyCascade(true, false);

			projection.SetActive(true);

			yield return new WaitForSeconds(projectionDuration);

			projection.SetActive(false);
			beam.SetActive(true);

			BossAudioInitializer.Instance.PlaySound(FireAttack);

			SetNegativeEnergyCascade(false, false);
			projection.SetActive(false);

			yield return new WaitForSeconds(beamDuration);

			if (isDeinitialized || this == null)
			{
				yield break;
			}

			beam.SetActive(false);
			RotateBeam(rotationAngle);
			RotateBeam(rotationAngle);

			projection.SetActive(false);
			yield return new WaitForSeconds(attackDelay);

			countOfAttacks--;
			SetNegativeEnergyCascade(false, false);

			Deinitialize();
		}

		Deinitialize();
		if (beamParent != null) Destroy(beamParent);
	}
	private void RotateBeam(float angle)
	{
		beamParent.transform.Rotate(0, 0, angle);
	}
}