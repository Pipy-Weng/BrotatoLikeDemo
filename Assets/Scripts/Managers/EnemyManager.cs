using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject chargerPrefab;

    private float timeBetweenSpawns = 0.5f;

    private float currentTimeBetweenSpawns;

    Transform enemiesParent;

    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        currentTimeBetweenSpawns = timeBetweenSpawns;
        enemiesParent = GameObject.Find("Enemies").transform;
    }

    private void Update()
    {
        if (!WaveManager.Instance.WaveRunning())
        {
            return;
        }
        currentTimeBetweenSpawns -= Time.deltaTime;

        if (currentTimeBetweenSpawns < 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    private void SpawnEnemy()
    {
        var roll = UnityEngine.Random.Range(0, 100);
        var enemyType = roll < 90 ? enemyPrefab : chargerPrefab;

        var e = Instantiate(enemyType, RandomPosition(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);

    }

    private Vector2 RandomPosition()
    {
        return new Vector2(UnityEngine.Random.Range(-16,16), UnityEngine.Random.Range(-8, 8));
    }

    public void DestroyAllEnemies()
    {
        foreach (Transform e in enemiesParent)
        {
            Destroy(e.gameObject);
        }
    }

}
