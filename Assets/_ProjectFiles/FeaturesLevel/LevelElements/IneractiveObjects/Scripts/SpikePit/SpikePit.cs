using UnityEngine;

public class SpikePit : MonoBehaviour
{
    public Transform teleportDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            collision.transform.position = teleportDestination.position;
        }
    }
}