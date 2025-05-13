using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour
{
	[SerializeField] private float _xStartPosition;
	[SerializeField] private float _xEndPosition;
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _delay = 1.0f;
	[SerializeField] private GameObject player;

	private bool _movingRight = true;
	private Vector3 _startPosition;
	private Vector3 _endPosition;

	private Vector3 _lastCloudPosition;

	private void Awake()
	{
		player = FindAnyObjectByType<PlayerController>().gameObject;
		_startPosition = transform.parent.TransformPoint(new Vector3(_xStartPosition, transform.localPosition.y, transform.localPosition.z));
		_endPosition = transform.parent.TransformPoint(new Vector3(_xEndPosition, transform.localPosition.y, transform.localPosition.z));

		transform.position = _startPosition;
		_lastCloudPosition = transform.position;

		Invoke(nameof(StartMovement), _delay);
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
			float duration = distance / _speed;

			float time = 0;
			Vector3 startPosition = transform.position;

			while (time < 1)
			{
				time += Time.deltaTime / duration;
				transform.position = Vector3.Lerp(startPosition, targetPosition, time);
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
			_playerAttached = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			_playerAttached = false;
		}
	}

	private bool _playerAttached = false;

	private void FixedUpdate()
	{
		if (_playerAttached && player != null)
		{
			Vector3 cloudMovement = transform.position - _lastCloudPosition;

			//player.transform.position += cloudMovement;
			player.transform.position = Vector3.Lerp(player.transform.position, player.transform.position + cloudMovement, Time.fixedDeltaTime * 2);
		}

		_lastCloudPosition = transform.position;
	}
}