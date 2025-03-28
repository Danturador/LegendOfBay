using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
	[SerializeField] private Transform player;
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
			
			Vector3 directionToPlayer = (player.position - transform.position).normalized;
			RotateToPlayer(directionToPlayer);

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
		float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x); // 1 или -1

		Vector3 targetPosition = transform.position + new Vector3(directionToPlayer * dashDistance, 0, 0);

		RotateToPlayer(new Vector3(directionToPlayer, 0, 0));

		yield return new WaitForSeconds(0.5f);

		float elapsedTime = 0f;
		float dashDuration = 1.5f;

		// Делаем дэш
		while (elapsedTime < dashDuration)
		{
			transform.position = new Vector3(
				Mathf.Lerp(transform.position.x, targetPosition.x, (elapsedTime / dashDuration)),
				transform.position.y,
				transform.position.z
			);

			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
	}
	public IEnumerator PerformComboAttack()
	{
		float attackDistance = 2.0f;
		int comboCount = 3;

		for (int i = 0; i < comboCount; i++)
		{
			Vector3 directionToPlayer = (player.position - transform.position).normalized;

			RotateToPlayer(directionToPlayer);

			Attack(attackDistance);

			yield return new WaitForSeconds(0.5f);
		}

		Debug.Log("Combo attack completed.");
	}

	private void Attack(float distance)
	{
		Vector3 directionToPlayer = (player.position - transform.position).normalized;

		Vector3 attackPosition = transform.position + directionToPlayer * distance;
		Debug.Log($"Attack at position: {attackPosition}");
	}

	private void RotateToPlayer(Vector3 directionToPlayer)
	{
		transform.rotation = Quaternion.Euler(0, directionToPlayer.x < 0 ? 0 : 180, 0);
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