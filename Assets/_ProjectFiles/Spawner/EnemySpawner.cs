using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameAssets.Scripts.Spawner
{
	[Serializable]
	public class EnemySpawner : MonoBehaviour
	{
		private static readonly int ClosePortal = Animator.StringToHash("ClosePortal");

		public string id;
		[SerializeField, Min(0f)] private float delayBetweenWaves = 3f;
		[SerializeField] private Animator animator;
		[SerializeField] private List<Wave> waveProfiles;
		private List<EnemyContainer> _enemies;
		private bool _startedSpawn;
		
		public bool isClosed;
		public event Action OnPortalClosed;

		public void Init(bool isClosed)
		{
			this.isClosed = isClosed;
			
			if(this.isClosed)
				gameObject.SetActive(false);
		}
		
		private void OnDestroy()
		{
			OnPortalClosed = null;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!isClosed && !_startedSpawn && other.gameObject.TryGetComponent(out PlayerController player))
			{
				_startedSpawn = true;
				SpawnEnemies();
			}
		}

		private void SpawnEnemies()
		{
			_enemies = new List<EnemyContainer>();
			StartCoroutine(TogglePortal());
		}

		private IEnumerator TogglePortal()
		{
			foreach (var wave in waveProfiles)
			{
				yield return StartCoroutine(SpawnWave(wave));
				yield return new WaitForSeconds(delayBetweenWaves);
			}

			yield return new WaitUntil(() => !_enemies.Any(e => e is null));

			Debug.Log("finish");
			
			animator.Play("Close");
			isClosed = true;
			OnPortalClosed?.Invoke();
		}

		private IEnumerator SpawnWave(Wave enemyProfiles)
		{
			List<int> enemyAmounts = enemyProfiles.enemiesOfWave
				.Select(enemySpawnerProfile => enemySpawnerProfile.amount).ToList();

			while (enemyAmounts.Any(x => x > 0))
			{
				int enemyIndex = Random.Range(0, enemyProfiles.enemiesOfWave.Count);
				while (enemyAmounts[enemyIndex] == 0)
					enemyIndex = Random.Range(0, enemyProfiles.enemiesOfWave.Count);

				enemyAmounts[enemyIndex]--;

				EnemyContainer enemyPrefab = enemyProfiles.enemiesOfWave[enemyIndex].enemyPrefab;
				EnemyContainer enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
				enemy.transform.position += Vector3.right * Random.Range(-2, 2);
				_enemies.Add(enemy);
				
				yield return new WaitForSeconds(Random.Range(1f, 2f));
			}
		}
	}
}