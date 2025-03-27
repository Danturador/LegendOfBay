using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyNavigationInfo : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        

        public float MoveSpeed => moveSpeed;
        
    }
}