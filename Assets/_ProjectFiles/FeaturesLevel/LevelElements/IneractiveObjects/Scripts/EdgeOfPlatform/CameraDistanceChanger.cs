using UnityEngine;
using Cinemachine;

public class CameraDistanceChanger : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private float followOffsetX;
	[SerializeField] private float followOffsetY;
	[SerializeField] private float distanceToSet;
	[SerializeField] private float lerpSpeed = 2f;
	[SerializeField] private float threshold = 0.02f;

	private readonly float defaultOffsetZ = -10f;
	private CinemachineTransposer cinemachineTransposer;
	private float originalDistance;
	private float targetDistance;
	private bool isChangingDistance;

	private void Start()
	{
		isChangingDistance = false;
		originalDistance = virtualCamera.m_Lens.OrthographicSize;
		targetDistance = originalDistance;
		cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
	}

	private void Update()
	{
		if (isChangingDistance)
		{
			virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetDistance, Time.deltaTime * lerpSpeed);

			Vector3 targetFollowOffset = new Vector3(followOffsetX, followOffsetY, defaultOffsetZ);
			cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * lerpSpeed);

			if (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetDistance) < threshold)
			{
				isChangingDistance = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			targetDistance = distanceToSet;
			isChangingDistance = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			targetDistance = originalDistance;
			isChangingDistance = true;

			followOffsetX = 0;
			followOffsetY = 0;
		}
	}
}