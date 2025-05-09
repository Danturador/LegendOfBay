using System;
using UnityEngine;

public class Key : MonoBehaviour
{
	public string keyID;
	public event Action OnKeyPickedUp;

	private void OnDestroy()
	{
		OnKeyPickedUp = null;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.TryGetComponent(out Inventory inventory)) 
			return;
		
		OnKeyPickedUp?.Invoke();
		inventory.AddKey(this);
		gameObject.SetActive(false);
	}

	public void SetState(bool isEnabled)
	{
		gameObject.SetActive(isEnabled);
	}
}