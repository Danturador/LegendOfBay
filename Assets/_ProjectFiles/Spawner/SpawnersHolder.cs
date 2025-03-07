using System;
using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.Spawner.Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GameAssets.Scripts.Spawner
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
                spawners[i].IsActive = spawnersHolderData.spawnersData[i].isActive;
            }
        }
        
        public List<SpawnerData> GetSpawnersData()
        {
            List<SpawnerData> spawnersData = new List<SpawnerData>();
            foreach (var spawner in spawners)
            {
                spawnersData.Add(new SpawnerData(spawner.IsActive));
            }

            return spawnersData;
        }
    }
}