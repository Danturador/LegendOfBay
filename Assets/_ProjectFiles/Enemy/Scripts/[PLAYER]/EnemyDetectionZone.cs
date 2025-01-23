using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts._PLAYER_
{
    public class EnemyDetectionZone : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private Camera playerCamera;
        public PlayerController Player => _player;

        private void Start()
        {
            boxCollider2D.size = new Vector2(2 * playerCamera.orthographicSize * playerCamera.aspect,
                2 * playerCamera.orthographicSize);
        }
    }
}