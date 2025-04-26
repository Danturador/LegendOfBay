using UnityEngine;

namespace _ProjectFiles.Menu.InGameMenuButtons
{
    public class InGameMenu : MonoBehaviour
    {
		[SerializeField] [Min(0f)] private float fadeDuration;
		private InputController _inputController;
        private Transform _child;

        private void Awake()
        {
            _child = transform.GetChild(0);
            _child.gameObject.SetActive(false);
			_inputController = FindAnyObjectByType<PlayerController>().inputController;
			_inputController.Gameplay.Escape.performed += ctx => ToggleState();
		}
		private void OnDestroy()
		{
			_inputController.Gameplay.Escape.performed -= ctx => ToggleState();
		}
        public void ToggleState()
        {
            var isActive = _child.gameObject.activeInHierarchy;
            _child.gameObject.SetActive(!isActive);
            ResetMenu();
        }

        private void ResetMenu()
        {
            _child.GetChild(0).gameObject.SetActive(true);
            _child.GetChild(1).gameObject.SetActive(false);
        }
    }
}