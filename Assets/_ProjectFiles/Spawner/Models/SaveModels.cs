using System;
using System.Collections.Generic;

namespace _ProjectFiles.Spawner.Models
{
    #region Data

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
    public class KeyData
    {
        public bool isActive = false;

        public KeyData(bool isActive)
        {
            this.isActive = isActive;
        }
    }
    
    [Serializable]
    public class DoorData
    {
        public bool isOpened = false;

        public DoorData(bool isOpened)
        {
            this.isOpened = isOpened;
        }
    }

    #endregion

    #region DataHolders

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
    
    [Serializable]
    public class KeysHolderData
    {
        public List<KeyData> keysData;

        public KeysHolderData()
        {
            keysData = new List<KeyData>();
        }
        
        public KeysHolderData(List<KeyData> keysData)
        {
            this.keysData = keysData;
        }
    }
    
    [Serializable]
    public class DoorsHolderData
    {
        public List<DoorData> doorsData;

        public DoorsHolderData()
        {
            doorsData = new List<DoorData>();
        }
        
        public DoorsHolderData(List<DoorData> doorsData)
        {
            this.doorsData = doorsData;
        }
    }

    #endregion
}