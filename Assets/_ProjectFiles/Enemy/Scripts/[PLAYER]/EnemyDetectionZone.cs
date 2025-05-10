using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts._PLAYER_
{
    public class EnemyDetectionZone : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private Camera playerCamera;
        public PlayerController Player => _player;
        public BoxCollider2D Collider => boxCollider2D;

        private void Start()
        {
            boxCollider2D.size = new Vector2(2 * playerCamera.orthographicSize * playerCamera.aspect,
                2 * playerCamera.orthographicSize);
        }
    }
}