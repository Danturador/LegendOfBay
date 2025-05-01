using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    [CreateAssetMenu(menuName = "Enemy/Info", fileName = "EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {
        [SerializeField] private EnemyType type;
        public EnemyType Type => type;
    }
}