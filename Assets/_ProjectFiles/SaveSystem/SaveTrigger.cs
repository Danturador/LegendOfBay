using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Zenject.SpaceFighter;

namespace _ProjectFiles.SaveSystem
{
    public class SaveTrigger : MonoBehaviour
    {
        public UnityEvent onSaveTriggered;
        [Inject] private SaveSystemController _saveSystemController;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Player player)) 
                return;
            
            _saveSystemController.UpdatePosition(player.Position);
            _saveSystemController.UpdateHealth(player.Health);
            onSaveTriggered?.Invoke();
        }
    }
}