using System.Collections.Generic;
using _GameAssets.Scripts.Spawner;
using UnityEngine;
using Zenject;

namespace _ProjectFiles.SaveSystem.InteractableHolders
{
    public class SpawnersHolder : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystem;
        [SerializeField] private List<EnemySpawner> spawners;

        private void Awake()
        {
            LoadSpawnersState(_saveSystem.gameData.SpawnersHolderData);
        }

        private void LoadSpawnersState(SpawnersHolderData spawnersHolderData)
        {
            foreach (var data in spawnersHolderData.spawnersData)
            {
                var spawner = spawners.Find(s => s.id == data.id);
                spawner.Init(data.isClosed);
            }

            foreach (var spawner in spawners)
            {
                spawner.OnPortalClosed += () => _saveSystem.UpdateSpawners(GetSpawnersData());
            }
        }
        
        private SpawnersHolderData GetSpawnersData()
        {
            List<SpawnerData> spawnersData = new List<SpawnerData>();
            foreach (var spawner in spawners)
            {
                spawnersData.Add(new SpawnerData(spawner.id, spawner.isClosed));
            }

            return new SpawnersHolderData(spawnersData);
        }
    }
}