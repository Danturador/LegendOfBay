using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _ProjectFiles.SaveSystem
{
    public class SaveTrigger : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystemController;
        public UnityEvent onSaveTriggered;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerController player)) 
                return;
            
            _saveSystemController.UpdatePosition(player.transform.position);
            _saveSystemController.UpdateHealth(100);
            onSaveTriggered?.Invoke();
        }
    }
}