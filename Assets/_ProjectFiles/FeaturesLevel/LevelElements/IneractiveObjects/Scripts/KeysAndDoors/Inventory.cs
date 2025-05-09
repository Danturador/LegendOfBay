using System.Collections.Generic;
using System.Linq;
using _GameAssets.Scripts.Spawner;
using _ProjectFiles.SaveSystem;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
	[Inject] private SaveSystemController _saveSystem;
	[SerializeField] private List<Key> keys = new List<Key>();
	[SerializeField] private KeysHolder keysHolder;
	
	private void Awake()
	{
		var activeKeys = _saveSystem.gameData.KeysHolderData.keysData.Where(k => k.isActive);
		foreach (var key in activeKeys)
		{
			AddKey(keys.Find(k => k.keyID == key.id));
		}
	}

	public void AddKey(Key key)
	{
		if (!HasKey(key.keyID))
		{
			keys.Add(key);
		}
	}

	private bool HasKey(string keyID)
	{
		return keys.Count > 0 && keys.Exists(k => k.keyID == keyID);
	}

	public Key GetKey(string keyID)
	{
		return keys.Count > 0 ? keys.Find(k => k.keyID == keyID) : null;
	}
}