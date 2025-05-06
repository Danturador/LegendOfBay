using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace _ProjectFiles.SaveSystem
{
    public class SaveTrigger : MonoBehaviour
    {
        [Inject] private SaveSystemController _saveSystemController;
        [Inject] private InputController _inputController;
        public UnityEvent onSaveTriggered;
		private PlayerController player;
		[SerializeField] private bool isGates;
		private bool isInCollader;
		private void Awake()
		{
			_inputController.Gameplay.UseAction.performed += ctx => SaveByButton();
			isInCollader = true;
		}
		private void OnDestroy()
		{
			_inputController.Gameplay.UseAction.performed -= ctx => SaveByButton();
		}
		private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out PlayerController player)) 
                return;

			this.player = player;

			if (!isGates)
			{
				_saveSystemController.UpdatePosition(player.transform.position);
				_saveSystemController.SaveProgress();
			}

			isInCollader = true;
        }
		private void OnTriggerExit2D(Collider2D other)
		{
			if (!other.gameObject.TryGetComponent(out PlayerController player))
				return;

			this.player = player;
			isInCollader = false;
		}
		private void SaveByButton()
		{
			if (isInCollader && isGates)
			{
				_saveSystemController.UpdatePosition(player.transform.position);
				_saveSystemController.UpdateHealth(100);
				_saveSystemController.SaveProgress();
				onSaveTriggered?.Invoke();
			}
		}
    }
}