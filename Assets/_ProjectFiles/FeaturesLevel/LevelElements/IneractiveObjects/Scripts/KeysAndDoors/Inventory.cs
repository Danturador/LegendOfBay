using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private List<Key> keys = new List<Key>();

	public void AddKey(Key key)
	{
		if (!HasKey(key.keyID))
		{
			keys.Add(key);
		}
	}

	public bool HasKey(string keyID)
	{
		if (keys.Count > 0)
		{
			return keys.Exists(k => k.keyID == keyID);
		}
		return false;
	}

	public Key GetKey(string keyID)
	{
		if (keys.Count > 0)
		{
			return keys.Find(k => k.keyID == keyID);
		}
		return null;
	}
}