using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyController whatToSpawn;
    [SerializeField] List<AreaObjectController> areaObjects;
    [SerializeField] private int numberOfLanes;
    [SerializeField] private int distanceBetweenLanes;
    [SerializeField] private float EnemySpawnInterval;
    private float timeSinceLastEnemySpawn = 0;
    private float timeSinceLastAreaObjectSpawn = 0;
    private ObjectPool<EnemyController> carPool;
    private ObjectPool<AreaObjectController> areaPool;
    [SerializeField] private Color[] colorPool;
    private float acclerateEnemies = 0;
    int carCounter = 0;

    private void Awake()
    {
        LoadDiffucltyLevel();
        AreaObjectController.OnOutOfBounds += OnOutOfBounds;
        carPool = new ObjectPool<EnemyController>(CreateNewCar);
        areaPool = new ObjectPool<AreaObjectController>(createNewAreaObject);
    }

    private void LoadDiffucltyLevel()
    {
        const int diffcult_factor = 5; // Easy =0 ,Medium =5, hard =10 (enemy acclaration factor)
        acclerateEnemies = PlayerPrefs.GetInt("DiffcultLevel")* diffcult_factor; //start from 0, 5 ,10 up to 20
        Debug.Log(acclerateEnemies);
    }
    void Update()
    {
        EnemyTimer();
        AreaTimer();
    }
    private void OnDestroy()
    {
        AreaObjectController.OnOutOfBounds -= OnOutOfBounds;
    }
    private void OnOutOfBounds(AreaObjectController obj) // OutOfBound event 
    {
        if (obj is EnemyController)
            HandleEnemyCarOutOfBounds(obj);
        else
            HandleAreaObjectOutOfBounds(obj);
    }

    #region EnemySpawner

    private EnemyController CreateNewCar()
        {
            return Instantiate(whatToSpawn, transform);
        }

        private void SpawnEnemy()
        {
            int laneNumber = UnityEngine.Random.Range(0, numberOfLanes); // Random pick a Lane To spawn
            EnemyController car = carPool.Get();
            car.AccelarateCarSpeed(acclerateEnemies); 
            car.transform.position = transform.position + Vector3.left * laneNumber * distanceBetweenLanes; // move enemy from enemy Pool to his current Lane
            car.SetColor(colorPool[UnityEngine.Random.Range(0, colorPool.Length)]); // Randomly change car color
            car.gameObject.SetActive(true);
        }

        private void HandleAreaObjectOutOfBounds(AreaObjectController obj)
        {
            obj.gameObject.SetActive(false);
            areaPool.Release(obj);
        }

        private void HandleEnemyCarOutOfBounds(AreaObjectController obj)
        {
            EnemyController car = obj as EnemyController;
            IncreaseDifficulty();
            car.gameObject.SetActive(false);
            carPool.Release(car);
        }

        private void IncreaseDifficulty()
        {
            carCounter++;
            if (carCounter % 5 == 0) // increase difficulty each 5 cars user has been passed
            {
                acclerateEnemies += 0.5f;// cars become quicker
                if (EnemySpawnInterval >= 0.5f) // up to 0.5 seconds till next spawn 
                    EnemySpawnInterval -= 0.1f;
            }
        }

        private void EnemyTimer()
        {
            timeSinceLastEnemySpawn += Time.deltaTime;
            if (timeSinceLastEnemySpawn > EnemySpawnInterval)
            {
                SpawnEnemy();
                timeSinceLastEnemySpawn = 0;
            }
        }
    #endregion

    #region AreaObjectSpawner

        private AreaObjectController createNewAreaObject()
        {
            int randomPickIndex = UnityEngine.Random.Range(0, areaObjects.Count);
            return Instantiate(areaObjects[randomPickIndex], transform);
        }


        private void AreaTimer()
        {
            timeSinceLastAreaObjectSpawn += Time.deltaTime;
            if (timeSinceLastAreaObjectSpawn > EnemySpawnInterval-0.2f)
            {
                SpawnAreaObject();
                timeSinceLastAreaObjectSpawn = 0;
            }
        }
        private void SpawnAreaObject()
        {
            int isRight = UnityEngine.Random.Range(0, 2); // Random pick a border to Spawn (right or left) 0 == left , 1 == right
            int distanceToBorder = UnityEngine.Random.Range(2, 5);
            Vector3 objPosition;
            if (isRight == 0)
                objPosition = transform.position + Vector3.left * (numberOfLanes * distanceBetweenLanes + distanceToBorder);
            else
                objPosition = transform.position + Vector3.right * distanceToBorder* distanceBetweenLanes/1.5f;

            AreaObjectController obj = areaPool.Get();
            obj.transform.position = objPosition; // move enemy from enemy Pool to his current Lane
            obj.AccelarateCarSpeed(acclerateEnemies); // 
            obj.gameObject.SetActive(true);
        }
    #endregion
}
