using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyController whatToSpawn;
    [SerializeField] private int numberOfLanes;
    [SerializeField] private int distanceBetweenLanes;
    [SerializeField] private float spawnInterval;
    private float timeSinceLastSpawn = 0;
    private ObjectPool<EnemyController> carPool;
    [SerializeField] private Color[] colorPool;
    private float acclerateEnemies = 0;
    int carCounter = 0;
    private void Awake()
    {
        LoadDiffucltyLevel();
        EnemyController.OnOutOfBounds += OnCarOutOfBounds;
        carPool = new ObjectPool<EnemyController>(CreateNewCar);
    }

    private void LoadDiffucltyLevel()
    {
        const int diffcult_factor = 5;
        acclerateEnemies = PlayerPrefs.GetInt("DiffcultLevel")* diffcult_factor; //start from 0, 5 ,10 up to 20
        Debug.Log(acclerateEnemies);
    }

    private void OnDestroy()
    {
        EnemyController.OnOutOfBounds -= OnCarOutOfBounds;
    }
    private EnemyController CreateNewCar()
    {
        return Instantiate(whatToSpawn, transform);

    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn > spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0;
        }
    }

    private void SpawnEnemy()
    {
        int laneNumber = UnityEngine.Random.Range(0, numberOfLanes);
        EnemyController car = carPool.Get();
        car.AccelarateCarSpeed(acclerateEnemies);
        car.transform.position = transform.position + Vector3.left * laneNumber * distanceBetweenLanes;
        car.SetColor(colorPool[UnityEngine.Random.Range(0, colorPool.Length)]);
        car.gameObject.SetActive(true);
    }

    private void OnCarOutOfBounds(EnemyController car)
    {
        setEnemeyCarSpeed();
        car.gameObject.SetActive(false);
        carPool.Release(car);
    }
    private void setEnemeyCarSpeed() // cars become quicker every time player gets score
    {
        carCounter++;
        if(carCounter%5==0) // increase difficulty each 5 cars user has been passed
        {
            acclerateEnemies += 0.5f;
            if(spawnInterval >=0.5f)
                spawnInterval -= 0.1f;
        }
    }
}
