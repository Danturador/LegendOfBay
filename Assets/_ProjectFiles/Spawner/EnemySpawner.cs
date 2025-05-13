using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameAssets.Scripts.Spawner
{
	[Serializable]
	public class EnemySpawner : MonoBehaviour
	{
		private static readonly int ClosePortal = Animator.StringToHash("Close");
		public string id;
		[SerializeField, Min(0f)] private float delayBetweenWaves = 3f;
		[SerializeField] private Animator animator;

		[SerializeField] private EnemyContainer hundunPrefab;
		[SerializeField] private Vector2Int spawnCounts;
		[SerializeField] private Vector2 spawnOffsetsX;
		[SerializeField] private Vector2 spawnOffsetsY;
		[SerializeField] private float spawnDelay;
		[SerializeField] private float waveDelay;
		[SerializeField] private int waveCount;
		private bool _startedSpawn;
		private int _currentEnemiesCount;

		public bool isClosed;
		public event Action OnPortalClosed;

		public void Init(bool isClosed)
		{
			this.isClosed = isClosed;
			if (this.isClosed)
				Destroy(gameObject);
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
			StartCoroutine(SpawnWave());
		}

		private IEnumerator SpawnWave()
		{
			var spawnCount = Random.Range(spawnCounts.x, spawnCounts.y + 1);
			for (var i = 0; i < spawnCount; i++)
			{
				var hundun = Instantiate(hundunPrefab, transform.position, Quaternion.identity);
				_currentEnemiesCount++;

				var targetPosition = transform.position;
				targetPosition.x += Random.Range(spawnOffsetsX.x, spawnOffsetsX.y);
				targetPosition.y += Random.Range(spawnOffsetsY.x, spawnOffsetsY.y);

				yield return new WaitUntil(() => hundun.IsInitialized);
				var navigation = hundun.Navigation.NavigationExecutable as HundunNavigation;
				hundun.StartCoroutine(navigation.SendToPoint(targetPosition));
				hundun.Health.OnDeath += OnEnemyDeath;

				yield return new WaitForSeconds(spawnDelay);
			}
			yield return new WaitForSeconds(waveDelay);
		}


		private void OnEnemyDeath()
		{
			_currentEnemiesCount--;
			if (_currentEnemiesCount == 0 && waveCount == 0)
			{
				animator.Play(ClosePortal);
				isClosed = true;
				OnPortalClosed?.Invoke();
			}
			else if (_currentEnemiesCount == 0)
			{
				waveCount--;
				SpawnEnemies();
			}
		}
	}
}