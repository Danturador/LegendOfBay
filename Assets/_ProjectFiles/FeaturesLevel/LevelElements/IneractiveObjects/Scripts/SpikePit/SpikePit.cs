using UnityEngine;

public class SpikePit : MonoBehaviour
{
	public Transform teleportDestination;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.name);

		PlayerHealth[] playerHealths = collision.GetComponentsInChildren<PlayerHealth>();

		foreach (var playerHealth in playerHealths)
		{
			Debug.Log(playerHealth.CurrentHealth);
			if (playerHealth.CurrentHealth > 1)
			{
				collision.transform.position = teleportDestination.position;
			}
		}
	}
}