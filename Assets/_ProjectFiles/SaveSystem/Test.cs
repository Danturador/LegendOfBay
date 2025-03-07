using _GameAssets.Scripts.Spawner;
using _ProjectFiles.Spawner.Models;
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
        [SerializeField] private SpawnersHolder spawnersHolder;
        [Inject] private SaveSystemController _saveSystem;

        private void Start()
        {
            saveButton.onClick.AddListener(SaveObjectData);
            loadButton.onClick.AddListener(LoadObjectData);
            spawnersHolder.Init();
        }

        private void OnDestroy()
        {
            saveButton.onClick.RemoveAllListeners();
            loadButton.onClick.RemoveAllListeners();
        }

        private void LoadObjectData()
        {
            var data = _saveSystem.LoadProgress();
            if (data is null)
            {
                Debug.LogError("Loaded data is null");
                return;
            }

            var gameData = (GameData)data;
            Debug.Log($"Health: {gameData.PlayerHealth}");
            obj.transform.position = gameData.Position;
            spawnersHolder.UpdateSpawnersState(gameData.SpawnersHolderData);
        }

        private void SaveObjectData()
        {
            _saveSystem.UpdateHealth(Time.time);
            _saveSystem.UpdatePosition(obj.transform.position);
            _saveSystem.UpdateSpawners(new SpawnersHolderData(spawnersHolder.GetSpawnersData()));
            _saveSystem.SaveProgress();
            Debug.Log("Saved");
        }
    }
}