using System;
using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine.Serialization;

namespace _GameAssets.Scripts.Spawner
{
    [Serializable]
    public class EnemySpawnerProfile
    {
        public int amount;
        public EnemyContainer enemyPrefab;
    }
}