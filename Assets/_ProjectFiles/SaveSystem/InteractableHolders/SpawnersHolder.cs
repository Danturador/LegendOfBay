using System;
using System.Collections.Generic;
using System.Linq;
using _GameAssets.Scripts.Spawner;
using UnityEngine;

namespace _ProjectFiles.SaveSystem.InteractableHolders
{
    [Serializable]
    public class SpawnersHolder : MonoBehaviour
    {
        [SerializeField] private List<EnemySpawner> spawners;
        public List<EnemySpawner> Spawners => spawners;

        public void Init()
        {
            spawners = GetComponentsInChildren<EnemySpawner>().ToList();
        }

        public void UpdateSpawnersState(SpawnersHolderData spawnersHolderData)
        {
            for(int i = 0; i < spawners.Count; i++)
            {
                spawners[i].isActive = spawnersHolderData.spawnersData[i].isActive;
            }
        }
        
        public List<SpawnerData> GetSpawnersData()
        {
            List<SpawnerData> spawnersData = new List<SpawnerData>();
            foreach (var spawner in spawners)
            {
                spawnersData.Add(new SpawnerData(spawner.isActive));
            }

            return spawnersData;
        }
    }
}