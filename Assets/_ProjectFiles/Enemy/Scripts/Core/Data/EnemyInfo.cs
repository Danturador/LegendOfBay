using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class EnemyInfo : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float provocationRadius;

        public float MaxHealth => maxHealth;
        public float ProvocationRadius => provocationRadius;
    }
}