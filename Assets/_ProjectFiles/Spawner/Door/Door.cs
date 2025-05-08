using UnityEngine;

namespace _GameAssets.Scripts.Spawner.Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Collider2D doorCollider;
        public bool isOpened;

        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isOpened || !other.TryGetComponent(out Player player))
                return;
            
            
        }
    }
}