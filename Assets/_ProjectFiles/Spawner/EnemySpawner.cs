using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameAssets.Scripts.Spawner
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private Transform parent;
		[SerializeField] private GameObject enemyPrefab;
		[SerializeField] private TextMeshProUGUI textStage;

		private List<IEnemyForSpawner> _enemies;
		public IReadOnlyCollection<IEnemyForSpawner> Enemies => _enemies;

		public void SpawnEnemiesRandomly()
		{
			_enemies = new List<IEnemyForSpawner>();
			Coroutine spawnCoroutine = StartCoroutine(StartSpawn(new List<EnemySpawnerProfile>()));
			StartCoroutine(WaitFightFinish(spawnCoroutine));
		}

		private IEnumerator StartSpawn(List<EnemySpawnerProfile> enemyProfiles)
		{
			//// TODO
			/// EnemyStageProfile contains amount of different enemies 
			List<int> enemyAmounts = enemyProfiles.Select(enemySpawnerProfile => enemySpawnerProfile.amount).ToList();

			while (enemyAmounts.Any(x => x > 0))
			{
				int enemyIndex = Random.Range(0, enemyProfiles.Count);
				while (enemyAmounts[enemyIndex] == 0)
					enemyIndex = Random.Range(0, enemyProfiles.Count);

				enemyAmounts[enemyIndex]--;

				////// TODO
				/// spawn enemy and initialize
				//_enemies.Add(enemy);

				yield return new WaitForSeconds(Random.Range(1f, 2f));
			}
		}

		private IEnumerator WaitFightFinish(Coroutine spawnCoroutine)
		{
			yield return spawnCoroutine;
			yield return new WaitUntil(() => _enemies.Count(x => x.IsAlive()) < 1);
		}
	}
}