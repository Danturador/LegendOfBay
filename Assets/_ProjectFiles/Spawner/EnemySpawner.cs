using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameAssets.Scripts.Spawner
{
	[Serializable]
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private List<Wave> waveProfiles;
		private List<IEnemyForSpawner> _enemies;
		public IReadOnlyCollection<IEnemyForSpawner> Enemies => _enemies;
		public bool IsActive;
		
		public void SpawnEnemiesRandomly()
		{
			_enemies = new List<IEnemyForSpawner>();
			foreach (var wave in waveProfiles)
			{
				Coroutine spawnCoroutine = StartCoroutine(StartSpawn(wave));
				StartCoroutine(WaitWaveSpawnFinish(spawnCoroutine));
			}
		}

		private IEnumerator StartSpawn(Wave enemyProfiles)
		{
			//// TODO
			/// EnemyStageProfile contains amount of different enemies 
			List<int> enemyAmounts = enemyProfiles.enemiesOfWave
				.Select(enemySpawnerProfile => enemySpawnerProfile.amount).ToList();

			while (enemyAmounts.Any(x => x > 0))
			{
				int enemyIndex = Random.Range(0, enemyProfiles.enemiesOfWave.Count);
				while (enemyAmounts[enemyIndex] == 0)
					enemyIndex = Random.Range(0, enemyProfiles.enemiesOfWave.Count);

				enemyAmounts[enemyIndex]--;

				////// TODO
				/// spawn enemy and initialize
				//_enemies.Add(enemy);

				yield return new WaitForSeconds(Random.Range(1f, 2f));
			}
		}

		private IEnumerator WaitWaveSpawnFinish(Coroutine spawnCoroutine)
		{
			yield return spawnCoroutine;
		}
	}
}