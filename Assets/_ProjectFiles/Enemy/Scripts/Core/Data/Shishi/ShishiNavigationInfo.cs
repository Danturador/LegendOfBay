using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core.Shishi
{
    [CreateAssetMenu(menuName = "Enemy/Shishi/Navigation info", fileName = "ShishiNavigationInfo")]
    public class ShishiNavigationInfo : EnemyNavigationInfo
    {
        [SerializeField] private float escapeRange;
        public float EscapeRange => escapeRange;
    }
}