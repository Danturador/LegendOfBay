using UnityEngine;
using Cinemachine;

public class CameraDistanceChanger : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private Vector3 followOffset;
	[SerializeField] private float distanceToSet;
	[SerializeField] private float lerpSpeed = 2f;

	private readonly Vector3 defaultOffset = new Vector3(0, 0, -10);
	private CinemachineTransposer cinemachineTransposer;
	private float originalDistance;
	private float targetDistance;

	private void Start()
	{
		originalDistance = virtualCamera.m_Lens.OrthographicSize;
		targetDistance = originalDistance;
		cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
	}

	private void Update()
	{
		// Плавное изменение расстояния
		virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetDistance, Time.deltaTime * lerpSpeed);

		// Плавное изменение следящего смещения
		cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetDistance == distanceToSet ? followOffset : defaultOffset, Time.deltaTime * lerpSpeed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			targetDistance = distanceToSet;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			targetDistance = originalDistance;
		}
	}
}