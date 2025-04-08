using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	[SerializeField] private string doorID;
	[SerializeField] private Animation gatesOpenAnimation;
	[SerializeField] private AnimationClip gatesAnimationClip;
	[SerializeField] private BoxCollider2D doorCollider;
	private bool isDoorsOpened;
	private void Awake() => isDoorsOpened = false;
	public bool TryOpen(Key key)
	{
		if (key != null)
		{
			if (key.keyID == doorID)
			{
				StartCoroutine(Open());
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

	private IEnumerator Open()
	{
		gatesOpenAnimation.Play();
		
		yield return new WaitForSeconds(gatesAnimationClip.length);
		
		doorCollider.enabled = false;
		isDoorsOpened = true;
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