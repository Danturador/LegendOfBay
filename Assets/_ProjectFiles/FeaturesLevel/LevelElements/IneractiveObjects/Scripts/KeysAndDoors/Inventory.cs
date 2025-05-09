using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.SaveSystem;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
	[Inject] private SaveSystemController _saveSystem;

	private HashSet<string> _keys = new();
	private bool _initialized;
	
	private void Awake()
	{
		_keys = _saveSystem.gameData.Inventory.ToHashSet();
	}

	public bool AddKey(string id)
	{
		if (!_keys.Add(id)) 
			return false;
		
		_saveSystem.UpdateInventory(_keys.ToList());
		return true;
	}

	public bool RemoveKey(string id)
	{
		if (!_keys.Remove(id)) 
			return false;
		
		_saveSystem.UpdateInventory(_keys.ToList());
		return true;
	}

	public string GetKeyId(string keyID)
	{
		return _keys.TryGetValue(keyID, out string value) ? value : null;
	}
}