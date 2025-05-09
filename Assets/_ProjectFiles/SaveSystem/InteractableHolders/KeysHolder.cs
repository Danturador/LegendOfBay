using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _ProjectFiles.SaveSystem.InteractableHolders
{
    public class KeysHolder : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystem;
        [SerializeField] private List<Key> keysOnMap;
        
        public void Awake()
        {
            LoadKeysState(_saveSystem.gameData.KeysHolderData);
        }

        private void LoadKeysState(KeysHolderData keysHolderData)
        {
            foreach (var data in keysHolderData.keysData)
            {
                var key = keysOnMap.Find(k => k.keyID == data.id);
                key.SetState(data.isActive);
            }

            foreach (var key in keysOnMap)
            {
                key.OnKeyPickedUp += () => _saveSystem.UpdateKeys(GetKeysData());
            }
        }
        
        private KeysHolderData GetKeysData()
        {
            List<KeyData> keysData = new List<KeyData>();
            foreach (var key in keysOnMap)
            {
                keysData.Add(new KeyData(key.keyID, key.IsActive));
            }

            return new KeysHolderData(keysData);
        }
    }
}