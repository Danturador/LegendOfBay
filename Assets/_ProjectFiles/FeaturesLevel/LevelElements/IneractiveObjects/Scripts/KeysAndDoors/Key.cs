using System;
using UnityEngine;

public class Key : MonoBehaviour
{
	public string keyID;
	public event Action OnKeyPickedUp;

	public bool IsActive => gameObject.activeInHierarchy;
	
	private void OnDestroy()
	{
		OnKeyPickedUp = null;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.TryGetComponent(out Inventory inventory)) 
			return;
		
		inventory.AddKey(keyID);
		gameObject.SetActive(false);
		OnKeyPickedUp?.Invoke();
	}

	public void SetState(bool isEnabled)
	{
		gameObject.SetActive(isEnabled);
	}
}