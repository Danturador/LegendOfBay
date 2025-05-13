using System;
using System.Collections;
using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy;
using _ProjectFiles.Enemy.Scripts.Core;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class HundunPortal : MonoBehaviour
{
    [SerializeField] private EnemyContainer hundunPrefab;
    [SerializeField] private Vector2Int spawnCounts;
    [SerializeField] private Vector2 spawnOffsetsX;
    [SerializeField] private Vector2 spawnOffsetsY;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float waveDelay;
    [SerializeField] private int waveCount;
    [SerializeField] private Transform spawnPosition;
    
    private bool _allowSpawn;
    private int _currentEnemiesCount;

    private void Start()
    {
        _allowSpawn = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
            if (_currentEnemiesCount == 0 && _allowSpawn)
                StartCoroutine(SpawnEnemies());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    private IEnumerator SpawnEnemies()
    {
        _allowSpawn = false;
        var spawnCount = Random.Range(spawnCounts.x, spawnCounts.y + 1);

        for (var i = 0; i < spawnCount; i++)
        {
            var hundun = Instantiate(hundunPrefab, spawnPosition.position, Quaternion.identity);
            hundun.Renderer.CurrentScale = -1;
            _currentEnemiesCount++;

            var targetPosition = transform.position;
            targetPosition.x -= Random.Range(spawnOffsetsX.x, spawnOffsetsX.y);
            targetPosition.y += Random.Range(spawnOffsetsY.x, spawnOffsetsY.y);

            yield return new WaitUntil(() => hundun.IsInitialized);
            var navigation = hundun.Navigation.NavigationExecutable as HundunNavigation;

            hundun.StartCoroutine(navigation.SendToPoint(targetPosition));
            hundun.Health.OnDeath += OnEnemyDeath;
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator WaveDelay()
    {
		if (waveCount > 1)
		{
			yield return new WaitForSeconds(waveDelay);

			_allowSpawn = true;
			waveCount--;
		}

    }
    
    private void OnEnemyDeath()
    {
        _currentEnemiesCount--;
        if (_currentEnemiesCount == 0)
        {
            StartCoroutine(WaveDelay());
        }
    }
}