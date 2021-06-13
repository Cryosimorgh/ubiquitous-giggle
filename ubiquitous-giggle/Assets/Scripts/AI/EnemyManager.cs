using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// Total amount of enemies to exist in the scene
    /// </summary>
    public int TotalEnemiesToSpawn = 5;

    /// <summary>
    /// List of transforms to spawn enemies from
    /// </summary>
    public List<Transform> SpawnLocations;

    [SerializeField]
    private GameObject _genericEnemyPrefab;

    private List<EnemyBase> _createdEnemies = new List<EnemyBase>();
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        // Spawn enemies on start
        if (_createdEnemies.Count < TotalEnemiesToSpawn)
        {
            for (int i = 0; i < TotalEnemiesToSpawn; i++)
            {
                SpawnNewEnemy();
            }
        }
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (_createdEnemies.Count > 0)
        {
            foreach(EnemyBase enemy in _createdEnemies)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    private void OnEnemyDied(EnemyBase enemy)
    {
        // Once enemy dies, spawn new one
        _createdEnemies.Remove(enemy);

        if (_createdEnemies.Count < TotalEnemiesToSpawn)
        {
            Debug.Log("Enemy died, spawning new Enemy");
            SpawnNewEnemy();
        }
    }

    /// <summary>
    /// Spawns a new enemy at a random spawn point
    /// </summary>
    /// <returns></returns>
    private bool SpawnNewEnemy()
    {
        if (_genericEnemyPrefab == null)
        {
            Debug.LogError("No prefab provided!");
            return false;
        }

        // Get a random spawn point from inside spawnlocations
        int rndSpawnPointIndex = Random.Range(0, SpawnLocations.Count);
        Transform spawnLoc = SpawnLocations[rndSpawnPointIndex];

        // Spawn enemy prefab
        GameObject enemy = Instantiate(_genericEnemyPrefab, spawnLoc.position, Quaternion.identity, null);

        // Get EnemyBase component
        EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
        if (enemyBase)
        {
            _createdEnemies.Add(enemyBase);

            // Listen to enemy death event
            enemyBase.OnEnemyDeath += this.OnEnemyDied;

            enemyBase.SetTarget(_player);

            return true;
        }
        else
        {
            Debug.LogError($"Instantiated enemy '{enemy.name}' doesn't contain EnemyBase script!");
            return false;
        }
    }
}
