using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
	public Transform player;
	[SerializeField] private float moveSpeed = 2f;
	[SerializeField] private float attackRange = 2f;
	public bool isReadyToAttack = false;
	public bool isAttack = false;

	private void Start()
	{
		
	}

	public IEnumerator MoveTowardsPlayerCoroutine()
	{
		while (!isReadyToAttack)
		{
			float distanceToPlayer = Vector3.Distance(transform.position, player.position);

			if (distanceToPlayer > attackRange)
			{
				Vector3 direction = (player.position - transform.position).normalized;
				transform.position += direction * moveSpeed * Time.deltaTime;
			}
			else
			{
				isReadyToAttack = true;
				isAttack = true;
				Debug.Log("Enemy attacks!");
			}

			yield return null;
		}
	}

	private void Attack()
	{

	}
}