using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] private string doorID;
	[SerializeField] private SpriteRenderer gates;
	[SerializeField] private Sprite openedGatesSprite;
	[SerializeField] private BoxCollider2D doorCollider;
	public bool TryOpen(Key key)
	{
		if (key != null)
		{
			if (key.keyID == doorID)
			{
				Open();
				return true;
			}
			else
			{
				Debug.Log("Этот ключ не подходит.");
			}
		}
		else
		{
			Debug.Log("Ключ отсутствует.");
		}
		return false;
	}

	private void Open()
	{
		gates.sprite = openedGatesSprite;
		doorCollider.enabled = false;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Inventory inventory = collision.GetComponent<Inventory>();
		if (inventory != null)
		{
			TryOpen(inventory.GetKey(doorID));
		}
	}
}