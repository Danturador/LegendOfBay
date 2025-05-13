using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.SaveSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Inventory : MonoBehaviour
{
	[Inject] private SaveSystemController _saveSystem;
	[SerializeField] private Image greenKey;
	[SerializeField] private Image redKey;

	private HashSet<string> _keys = new();
	private int _keysCollected;
	
	private void Awake()
	{
		_keys = _saveSystem.gameData.Inventory.ToHashSet();
		_keysCollected = _saveSystem.gameData.keysCollected;
		ShowKeyIcons();
	}

	public bool AddKey(string id)
	{
		if (!_keys.Add(id)) 
			return false;

		_keysCollected++;

		_saveSystem.UpdateInventory(_keys.ToList());
		_saveSystem.UpdateCollectedKeysCount(_keysCollected);
		ShowKeyIcons();
		return true;
	}

	public bool RemoveKey(string id)
	{
		if (!_keys.Remove(id)) 
			return false;
		
		_saveSystem.UpdateInventory(_keys.ToList());
		ShowKeyIcons();
		return true;
	}

	public string GetKeyId(string keyID)
	{
		return _keys.TryGetValue(keyID, out string value) ? value : null;
	}
	public bool HasKey(string keyID)
	{
		return _keys.Contains(keyID);
	}

	private void ShowKeyIcons()
	{
		greenKey.gameObject.SetActive(HasKey("0"));
		redKey.gameObject.SetActive(HasKey("1"));
	}
}