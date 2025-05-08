using System;
using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.Spawner.Models;
using UnityEngine;

namespace _GameAssets.Scripts.Spawner
{
    [Serializable]
    
    public class KeysHolder : MonoBehaviour
    {
        [SerializeField] private List<Key> keys;
        public List<Key> Keys => keys;

        public void Init()
        {
            keys = GetComponentsInChildren<Key>().ToList();
        }

        public void UpdateSpawnersState(KeysHolderData keysHolderData)
        {
            for(int i = 0; i < keys.Count; i++)
            {
                keys[i].isActive = keysHolderData.keysData[i].isActive;
            }
        }
        
        public List<KeyData> GetKeysData()
        {
            List<KeyData> keysData = new List<KeyData>();
            foreach (var key in keys)
            {
                keysData.Add(new KeyData(key.isActive));
            }

            return keysData;
        }
    }
}