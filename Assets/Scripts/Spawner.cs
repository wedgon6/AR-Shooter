using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Enemy[] _enemies;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Enemy newEnemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], GetSpawnPoint(_spawnRadius),Quaternion.identity);
            Vector3 lookDiration = _target.transform.position - newEnemy.transform.position;
            newEnemy.transform.rotation = Quaternion.LookRotation(lookDiration);
            newEnemy.Dying += OnEnemyDying;

            yield return new WaitForSeconds(_secondsBetweenSpawn);
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _target.AddScore();
        enemy.Dying -= OnEnemyDying;
    }

    private Vector3 GetSpawnPoint(float radius)
    {
        return Random.insideUnitSphere* radius;
    }
}
