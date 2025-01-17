using System;

namespace _GameAssets.Scripts.Spawner
{
    /// <summary>
    /// This class will be changed and contains info about concrete enemy in spawner
    /// </summary>
    [Serializable]
    public class EnemySpawnerProfile
    {
        public int amount;
        public IEnemyForSpawner EnemyForSpawnerPrefab;
    }
}