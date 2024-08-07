using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum FactoryModes
{
    Fixed,
    Random
}

public class Factory : MonoBehaviour
{
    public static Action WaveFinished;

    [Header("Settings")]
    [SerializeField] private FactoryModes factoryMode = FactoryModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;


    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwFactories;

    [Header("Random Delay")]
    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    [Header("Capsules")]
    [SerializeField] private ObjectCapsule enemyWave10Capsule;
    [SerializeField] private ObjectCapsule enemyWave11to20Capsule;
    [SerializeField] private ObjectCapsule enemyWave21to30Capsule;

    private float _factoryTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;

    private TransferPoint _transferPoint;

    public int CurrentWave { get; set; }

    private void Start()
    {
        _transferPoint = GetComponent<TransferPoint>();

        _enemiesRamaining = enemyCount;
    }

    void Update()
    {
        _factoryTimer -= Time.deltaTime;
        if (_factoryTimer < 0)
        {
            _factoryTimer = GetFactoryDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                FactoryEnemy();
            }
        }

    }



    private void FactoryEnemy()
    {

        GameObject newInstance = GetCapsule().GetInstanceFromCapsule();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.TransferPoint = _transferPoint;
        enemy.AgainEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }

    private float GetFactoryDelay()
    {
        float delay = 0f;
        if (factoryMode == FactoryModes.Fixed)
        {
            delay = delayBtwFactories;
        }
        else
        {
            delay = GetRandomDelay();
        }
        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minDelay, maxDelay);
        return randomTimer;
    }

    public ObjectCapsule GetCapsule()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave <= 10)
        {
            return enemyWave10Capsule;
        }
        if (currentWave > 10 && currentWave <= 20)
        {
            return enemyWave11to20Capsule;
        }

        if (currentWave > 20 && currentWave <= 30)
        {
            return enemyWave21to30Capsule;
        }

        return null;
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwFactories);
        _enemiesRamaining = enemyCount;
        _factoryTimer = 0f;
        _enemiesSpawned = 0;
    }

    private void RecordEnemyEndReached(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0)
        {
            WaveFinished?.Invoke();
            if (LevelManager.Instance.CurrentWave >= 30)
            {
                UnlockNewLevel();
                SceneController.instance.NextLevel();
            }
            else
            {
                StartCoroutine(NextWave());
            }
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();

        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemyEndReached;
        EnemyHealth.OnEnemyKilled += RecordEnemyEndReached;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemyEndReached;
        EnemyHealth.OnEnemyKilled -= RecordEnemyEndReached;

    }
}


