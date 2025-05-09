using UnityEngine;
using Zenject;

namespace _ProjectFiles.SaveSystem.InteractableHolders
{
    public class GrapplingHookHolder : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystem;

        private void Awake()
        {
            gameObject.SetActive(!_saveSystem.gameData.HaveGrapplingHook);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerController playerController)) 
                return;
            
            playerController.ReceiveHook();
            _saveSystem.UpdateHookState(true);
            gameObject.SetActive(false);
        }
    }
}