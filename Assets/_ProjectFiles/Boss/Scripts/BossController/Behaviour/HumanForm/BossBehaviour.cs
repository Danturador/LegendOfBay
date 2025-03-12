using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
	public Transform player;
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float attackRange = 2f;
	[SerializeField] private float dashDistance = 7f;
	public bool isReadyToAttack = false;
	public bool isPlayerNear = false;

	public IEnumerator MoveTowardsPlayerCoroutine()
	{
		float randomTime = Random.Range(0f, 1f);
		float elapsedTime = 0f;

		while (!isReadyToAttack && elapsedTime < randomTime)
		{
			float distanceToPlayer = Vector3.Distance(transform.position, player.position);

			if (distanceToPlayer > attackRange)
			{
				Vector3 direction = (player.position - transform.position).normalized;
				transform.position += direction * moveSpeed * Time.deltaTime;

				elapsedTime += Time.deltaTime;
			}
			else
			{
				isReadyToAttack = true;
				Debug.Log("Enemy attacks!");
				break;
			}

			yield return null;
		}
	}

	public IEnumerator DashTowardsPlayer()
	{
		Vector3 direction = (player.position - transform.position).normalized;

		Vector3 targetPosition = transform.position + direction * dashDistance;

		float elapsedTime = 0f;
		float dashDuration = 1.5f;

		while (elapsedTime < dashDuration)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition, (elapsedTime / dashDuration));
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = targetPosition;	
	}
	public IEnumerator PerformComboAttack()
	{
		float attackDistance = 2.0f; 
		int comboCount = 3;

		for (int i = 0; i < comboCount; i++)
		{
			Attack(attackDistance);

			yield return new WaitForSeconds(0.5f);
		}

		Debug.Log("Combo attack completed.");
	}

	private void Attack(float distance)
	{
		Vector3 direction = (player.position - transform.position).normalized;

		//temp
		Vector3 attackPosition = transform.position + direction * distance;
		Debug.Log($"Attack at position: {attackPosition}");

		// Damage
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
			isPlayerNear = true;
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
			isPlayerNear = false;
	}
}