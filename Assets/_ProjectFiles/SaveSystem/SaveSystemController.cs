using _GameAssets.Scripts.Spawner;
using _ProjectFiles.Spawner.Models;
using UnityEngine;

namespace _ProjectFiles.SaveSystem
{
    public class SaveSystemController
    {
        private readonly IStorage _storage;
        private readonly GameData _data;
        
        public SaveSystemController(IStorage storage)
        {
            _storage = storage;
            _data = (GameData)_storage.Load(new GameData(), PlayerPrefs.GetInt(Storage.PrefsKey) == 0);
        }

        public void SaveProgress() => _storage.Save(_data);

        public object LoadProgress() => _storage.Load(_data);
        
        public void UpdatePosition(Vector3 position) => _data.SetPosition(position);

        public void UpdateHealth(float health) => _data.SetPlayerHealth(health);
        
        public void UpdateSpawners(SpawnersHolderData data) => _data.SetSpawners(data);
    }
}