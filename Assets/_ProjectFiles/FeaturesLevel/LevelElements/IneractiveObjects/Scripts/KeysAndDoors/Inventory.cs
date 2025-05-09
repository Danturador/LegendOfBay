using System.Collections.Generic;
using System.Linq;
using _GameAssets.Scripts.Spawner;
using _ProjectFiles.SaveSystem;
using _ProjectFiles.SaveSystem.InteractableHolders;
using UnityEngine;
using Zenject;

public class Inventory : MonoBehaviour
{
	[Inject] private SaveSystemController _saveSystem;
	//[SerializeField] private List<Key> keys = new List<Key>();
	[SerializeField] private KeysHolder keysHolder;

	private readonly HashSet<string> _keys = new();
	
	private void Awake()
	{
		var ownedKeys = _saveSystem.gameData.KeysHolderData.keysData.Where(k => !k.isActive);
		foreach (var key in ownedKeys)
		{
			AddKey(key.id);
		}
	}

	public void AddKey(string id)
	{
		_keys.Add(id);
	}

	public string GetKeyId(string keyID)
	{
		return _keys.TryGetValue(keyID, out string value) ? value : null;
	}
}