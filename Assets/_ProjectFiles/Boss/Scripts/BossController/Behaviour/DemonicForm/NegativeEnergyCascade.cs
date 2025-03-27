using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private void Deinitialize()
	{
		Destroy(projection);
		Destroy(beam);
	}
	public IEnumerator AttackPattern()
	{
		Initialize();

		while (countOfAttacks > 0)
		{
			projection.SetActive(true);

			yield return new WaitForSeconds(projectionDuration);

			projection.SetActive(false);
			beam.SetActive(true);

			yield return new WaitForSeconds(beamDuration);
			beam.SetActive(false);
			RotateBeam(projection.transform, rotationAngle);
			RotateBeam(beam.transform, rotationAngle);

			yield return new WaitForSeconds(attackDelay);

			countOfAttacks--;
			//StartCoroutine(CreateNegativeEnergyCascade());
		}

		Deinitialize();
	}
	public IEnumerator CreateNegativeEnergyCascade()
	{
		while (countOfAttacks > 0)
		{
			projection.SetActive(true);

			yield return new WaitForSeconds(projectionDuration);

			projection.SetActive(false);
			beam.SetActive(true);

			yield return new WaitForSeconds(beamDuration);
			beam.SetActive(false);
			RotateBeam(projection.transform, rotationAngle);
			RotateBeam(beam.transform, rotationAngle);

			yield return new WaitForSeconds(attackDelay);

			countOfAttacks--;
			//StartCoroutine(CreateNegativeEnergyCascade());
		}
	}
	private void RotateBeam(Transform transform, float angle)
	{
		transform.Rotate(0, 0, angle);
	}
}