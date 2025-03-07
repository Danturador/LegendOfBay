using System;
using System.Collections.Generic;

namespace _ProjectFiles.Spawner.Models
{
    [Serializable]
    public class SpawnerData
    {
        public bool isActive = false;

        public SpawnerData(bool isActive)
        {
            this.isActive = isActive;
        }
    }
    
    [Serializable]
    public class SpawnersHolderData
    {
        public List<SpawnerData> spawnersData;

        public SpawnersHolderData()
        {
            spawnersData = new List<SpawnerData>();
        }
        
        public SpawnersHolderData(List<SpawnerData> spawnersData)
        {
            this.spawnersData = spawnersData;
        }
    }
}