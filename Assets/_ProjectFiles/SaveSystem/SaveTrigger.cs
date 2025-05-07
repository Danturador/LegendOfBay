using UnityEngine;
using UnityEngine.Events;
using Zenject;
using System.Collections;

namespace _ProjectFiles.SaveSystem
{
	public class SaveTrigger : MonoBehaviour
	{
		[Inject] private SaveSystemController _saveSystemController;
		[Inject] private InputController _inputController;
		[SerializeField] private Animation showMessageAnimation;
		public UnityEvent onSaveTriggered;
		private PlayerController player;
		[SerializeField] private bool isGates;
		private bool isInCollader;
		private bool canSave = true;
		[SerializeField] private float saveCooldown = 2.0f;

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
				BasicSave();
			}

			isInCollader = true;
		}
		private void OnTriggerExit2D(Collider2D other)
		{
			if (!other.gameObject.TryGetComponent(out PlayerController player))
				return;

			this.player = null;
			isInCollader = false;
		}
		private void SaveByButton()
		{
			if (isInCollader && isGates && this.player != null && canSave)
			{
				_saveSystemController.UpdateHealth(100);
				BasicSave();
				onSaveTriggered?.Invoke();
				StartCoroutine(SaveCooldown());
			}
		}
		private void BasicSave()
		{
			_saveSystemController.UpdatePosition(player.transform.position);
			_saveSystemController.SaveProgress();
			showMessageAnimation.Play();
		}

		private IEnumerator SaveCooldown()
		{
			canSave = false;
			yield return new WaitForSeconds(saveCooldown);
			canSave = true;
		}
	}
}