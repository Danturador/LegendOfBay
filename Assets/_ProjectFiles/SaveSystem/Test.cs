using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _ProjectFiles.SaveSystem
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [Inject] private IStorage _storage;
        private GameData _gameData;
        
        private void Start()
        {
            _gameData = new GameData();
            saveButton.onClick.AddListener(SaveObjectData);
            loadButton.onClick.AddListener(LoadObjectData);
        }

        private void OnDestroy()
        {
            saveButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();
        }

        private void LoadObjectData()
        {
            object data = _storage.Load();
            if (data is null)
            {
                Debug.LogError("Loaded data is null");
                return;
            }
            
            _gameData = (GameData)data;
            Debug.Log($"Health: {_gameData.health}");
            obj.transform.position = _gameData.position;
        }
        
        private void SaveObjectData()
        {
            _gameData.health = Time.time;
            _gameData.position = obj.transform.position;
            _storage.Save(_gameData);
            Debug.Log("Saved");
        }
    }
}