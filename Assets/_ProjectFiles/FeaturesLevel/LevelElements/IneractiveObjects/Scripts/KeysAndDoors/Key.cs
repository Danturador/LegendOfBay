using UnityEngine;

public class Key : MonoBehaviour
{
	public string keyID;
	private Inventory inventory;

	public Key(string id)
	{
		keyID = id;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		inventory = other.GetComponent<Inventory>();
		
		if (inventory != null)
		{
			inventory.AddKey(this);
			gameObject.SetActive(false);
		}
	}
}