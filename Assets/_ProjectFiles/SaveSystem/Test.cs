using Unity.VisualScripting;
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
        [Inject] private SaveSystemController _saveSystem;
        
        private void Start()
        {
            LoadObjectData();
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
            var data = _saveSystem.LoadProgress();
            if (data is null)
            {
                Debug.LogError("Loaded data is null");
                return;
            }

            var gameData = (GameData)data;
            Debug.Log($"Health: {gameData.PlayerHealth}");
            obj.transform.position = gameData.Position;
        }

        private void SaveObjectData()
        {
            _saveSystem.UpdateHealth(Time.time);
            _saveSystem.UpdatePosition(obj.transform.position);
            _saveSystem.SaveProgress();
            Debug.Log("Saved");
        }
    }
}