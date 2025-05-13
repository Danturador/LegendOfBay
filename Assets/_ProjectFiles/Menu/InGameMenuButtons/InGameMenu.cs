using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameMenu : MonoBehaviour
    {
		[SerializeField] [Min(0f)] private float fadeDuration;
		[Inject] private InputController _inputController;
        private Transform _child;
        private static float timeScaleValue = 0;

        private void Awake()
        {
            _child = transform.GetChild(0);
            _child.gameObject.SetActive(false);
			//_inputController = FindAnyObjectByType<PlayerController>().inputController;
			_inputController.UI.Escape.performed += ToggleState;
		}
		private void OnDestroy()
		{
			_inputController.UI.Escape.performed -= ToggleState;
		}
        public void ToggleState(InputAction.CallbackContext context)
        {
            var isActive = _child.gameObject.activeInHierarchy;
            _child.gameObject.SetActive(!isActive);

			if (!isActive)
			{
				timeScaleValue = Time.timeScale;
				Time.timeScale = 0;
				_inputController.Gameplay.Disable();
			}
			else
			{
				RestoreTime();
				_inputController.Gameplay.Enable(); 
			}

			ResetMenu();
        }
		public static void RestoreTime()
		{
			Time.timeScale = timeScaleValue;
		}

        private void ResetMenu()
        {
            _child.GetChild(0).gameObject.SetActive(true);
            _child.GetChild(1).gameObject.SetActive(false);
        }
    }
}