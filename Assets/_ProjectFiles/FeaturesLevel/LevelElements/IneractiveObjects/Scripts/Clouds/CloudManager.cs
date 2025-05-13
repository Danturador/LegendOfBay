using UnityEngine;
using System.Collections;
using Zenject;

public class CloudManager : MonoBehaviour
{
	[Inject] private InputController _inputController;
	[SerializeField] private float _xStartPosition;
	[SerializeField] private float _xEndPosition;
	[SerializeField] private AnimationCurve _speedCurve;
	[SerializeField] private float _delay = 1.0f;
	[SerializeField] private GameObject player;

	private bool _movingRight = true;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private Vector3 _lastCloudPosition;
	private bool _isPlayerAttached = false;
	private bool _isInput = false;

	private void Awake()
	{
		player = FindAnyObjectByType<PlayerController>().gameObject;
		_startPosition = transform.parent.TransformPoint(new Vector3(_xStartPosition, transform.localPosition.y, transform.localPosition.z));
		_endPosition = transform.parent.TransformPoint(new Vector3(_xEndPosition, transform.localPosition.y, transform.localPosition.z));

		transform.position = _startPosition;
		_lastCloudPosition = transform.position;

		Invoke(nameof(StartMovement), _delay);

		_inputController.Gameplay.Movement.started += ctx => ToggleMovement(true);
		_inputController.Gameplay.Dash.started += ctx => ToggleMovement(true);
		_inputController.Gameplay.Jump.started += ctx => ToggleMovement(true);

		_inputController.Gameplay.Movement.canceled += ctx => ToggleMovement(false);
		_inputController.Gameplay.Dash.canceled += ctx => ToggleMovement(false);
		_inputController.Gameplay.Jump.canceled += ctx => ToggleMovement(false);
	}
	private void ToggleMovement(bool needMove)
	{
		_isInput = needMove;
	}
	private void StartMovement()
	{
		StartCoroutine(Move());
	}

	private IEnumerator Move()
	{
		while (true)
		{
			Vector3 targetPosition = _movingRight ? _endPosition : _startPosition;
			float distance = Vector3.Distance(transform.position, targetPosition);
			float duration = distance / _speedCurve.Evaluate(0);

			float time = 0;
			Vector3 startPosition = transform.position;

			while (time < duration)
			{
				time += Time.deltaTime;
				float normalizedTime = time / duration;
				transform.position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
				if (_isPlayerAttached && !_isInput && player != null)
				{
					Vector3 cloudMovement = transform.position - _lastCloudPosition;

					player.transform.position += cloudMovement;
				}
				_lastCloudPosition = transform.position;
				yield return null;
			}

			_movingRight = !_movingRight;
			yield return null;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			_isPlayerAttached = true;
			_isInput = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			_isPlayerAttached = false;
			_isInput = true;
		}
	}

	private void FixedUpdate()
	{
		//if (_playerAttached && player != null)
		//{
		//	Vector3 cloudMovement = transform.position - _lastCloudPosition;

		//	// Smoothly move the player with the cloud movement
		//	Debug.LogError($"{player.transform.position} + {cloudMovement} = { player.transform.position + cloudMovement}");
		//	player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + cloudMovement, Time.fixedDeltaTime * 5);
		//}

		//_lastCloudPosition = transform.position;
	}
} 