using System;
using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.SaveSystem;
using _ProjectFiles.Spawner.Models;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _GameAssets.Scripts.Spawner
{
    public class KeysHolder : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystem;
        [SerializeField] private List<Key> keysOnMap;

        public void Awake()
        {
            keysOnMap ??= GetComponentsInChildren<Key>().ToList();
            UpdateSpawnersState(_saveSystem.gameData.KeysHolderData);
        }

        private void UpdateSpawnersState(KeysHolderData keysHolderData)
        {
            foreach (var data in keysHolderData.keysData)
            {
                var key = keysOnMap.Find(k => k.keyID == data.id);
                key.SetState(data.isActive);
                key.OnKeyPickedUp += () => _saveSystem.gameData.SetKeys(GetKeysData());
            }
        }
        
        public KeysHolderData GetKeysData()
        {
            List<KeyData> keysData = new List<KeyData>();
            foreach (var key in keysOnMap)
            {
                keysData.Add(new KeyData(key.keyID, key.gameObject.activeInHierarchy));
            }

            return new KeysHolderData(keysData);
        }
    }
}