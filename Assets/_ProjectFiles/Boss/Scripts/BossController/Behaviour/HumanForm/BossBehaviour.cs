using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float attackRange = 2f;
	[SerializeField] private float dashDistance = 7f;

	[SerializeField] private Collider2D dashCollider;
	[SerializeField] private Collider2D swordSlashCollider;

	public bool isReadyToAttack = false;
	public bool isPlayerNear = false;
	private float[] attacksLength = new float[3] { 0.7f, 0.75f, 0.6f };
	private void Awake()
	{
		isPlayerNear = false;
		dashCollider.enabled = false;
		swordSlashCollider.enabled = false;
	}
	public IEnumerator MoveTowardsPlayerCoroutine()
	{
		Debug.LogError("start walking");
		float randomTime = Random.Range(0f, 1f);
		float elapsedTime = 0f;

		while (/*!isReadyToAttack && */elapsedTime < randomTime)
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
				//isReadyToAttack = true;
				//Debug.Log("Enemy attacks!");
				break;
			}

			yield return null;
		}
		Debug.LogError("stop walking");
	}

	public IEnumerator DashTowardsPlayer()
	{
		float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);

		Vector3 targetPosition = transform.position + new Vector3(directionToPlayer * dashDistance, 0, 0);

		RotateToPlayer(new Vector3(directionToPlayer, 0, 0));

		yield return new WaitForSeconds(0.5f);

		float elapsedTime = 0f;
		float dashDuration = 1f;

		dashCollider.enabled = true;
		Invoke(nameof(ChangeDashColliderState), dashDuration - 0.5f);

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
		dashCollider.enabled = false;
	}
	private void ChangeDashColliderState()
	{
		dashCollider.enabled = false;
	}
	public IEnumerator PerformComboAttack()
	{
		int comboCount = 3;

		for (int i = 0; i < comboCount; i++)
		{
			Vector3 directionToPlayer = (player.position - transform.position).normalized;

			RotateToPlayer(directionToPlayer);
			float colliderDuration = attacksLength[i] / 3;

			yield return new WaitForSeconds(colliderDuration);

			swordSlashCollider.enabled = true;

			yield return new WaitForSeconds(colliderDuration);

			swordSlashCollider.enabled = false;

			yield return new WaitForSeconds(colliderDuration);
		}
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