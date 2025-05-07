using UnityEngine;
using System.Collections;

public class CloudManager : MonoBehaviour
{
	[SerializeField] private float _xStartPosition;
	[SerializeField] private float _xEndPosition;
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _delay = 1.0f;

	private bool _movingRight = true;
	private Vector3 _startPosition;
	private Vector3 _endPosition;

	private void Awake()
	{
		_startPosition = transform.parent.TransformPoint(new Vector3(_xStartPosition, transform.localPosition.y, transform.localPosition.z));
		_endPosition = transform.parent.TransformPoint(new Vector3(_xEndPosition, transform.localPosition.y, transform.localPosition.z));

		transform.position = _startPosition;

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
}