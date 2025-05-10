using System.Collections.Generic;
using UnityEngine;

namespace _ProjectFiles.SaveSystem
{
    public class SaveSystemController
    {
        private readonly IStorage _storage;
        private readonly GameData _data;
		public GameData gameData => _data;
        
        public SaveSystemController(IStorage storage)
        {
            _storage = storage;
            _data = (GameData)_storage.Load(new GameData(), PlayerPrefs.GetInt(Storage.PrefsKey) == 0);
        }

        public void SaveProgress() => _storage.Save(_data);

        public object LoadProgress() => _storage.Load(_data);
        
        
        public void UpdatePosition(Vector3 position) => _data.SetPosition(position);

        public void UpdateHealth(float health) => _data.SetPlayerHealth(health);

        public void UpdateHookState(bool hasHook) => _data.SetGrapplingHook(hasHook);
        
        public void UpdateSpawners(SpawnersHolderData data) => _data.SetSpawners(data);
        
        public void UpdateKeys(KeysHolderData data) => _data.SetKeys(data);
        
        public void UpdateDoors(DoorsHolderData data) => _data.SetDoors(data);

        public void UpdateInventory(List<string> data) => _data.SetInventory(data);
        
        public void UpdateTexture(Texture2D texture2D) => _data.SetTexture(texture2D);
    }
}