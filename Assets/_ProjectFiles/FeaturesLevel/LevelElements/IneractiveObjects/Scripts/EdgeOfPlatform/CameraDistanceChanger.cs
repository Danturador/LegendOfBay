using UnityEngine;
using Cinemachine;

public class CameraDistanceChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float distanceToSet;

    private readonly Vector3 defaultOffset = new Vector3(0, 0, -10);
    private CinemachineTransposer cinemachineTransposer;
    private float originalDistance;

    private void Start()
    {
        originalDistance = virtualCamera.m_Lens.OrthographicSize;
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.GetComponent<PlayerController>() != null);
        if (collision.GetComponent<PlayerController>() != null)
        {
            cinemachineTransposer.m_FollowOffset = followOffset;
            virtualCamera.m_Lens.OrthographicSize = distanceToSet;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            cinemachineTransposer.m_FollowOffset = defaultOffset;
            virtualCamera.m_Lens.OrthographicSize = originalDistance;
        }
    }
}