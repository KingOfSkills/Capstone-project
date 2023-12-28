using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public static int zombiesAlive = 0;
    private int zombiesToSpawn;
    private int popoZombiesToSpawn;
    private int explodersToSpawn;
    [SerializeField] private float delayTime = 1f;

    //private Vector3 spawnPosition;
    // References to Zombie Prefabs
    [SerializeField] private GameObject mindlessZombie;
    [SerializeField] private GameObject zombie;
    [SerializeField] private GameObject popoZombie;
    [SerializeField] private GameObject exploder;
    // SpawnBoss
    // Reference to Zombie Boss Prefab
    [SerializeField] private GameObject boss;
    //public static bool isBossSpawned = false;
    private bool canSpawnPoPoZombie;
    private bool canSpawnExploder;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        zombiesAlive = 0;
        canSpawnPoPoZombie = false;
        canSpawnExploder = false;
    }

    private void Update()
    {
        if (zombiesAlive <= 0 &&
            WaveManager.isWaveInProgress == true &&
            zombiesToSpawn == 0 &&
            popoZombiesToSpawn == 0 &&
            explodersToSpawn == 0)// && state == SpawnState.Waiting)
        {
            WaveManager.isWaveInProgress = false;
        }
    }

    private void SpawnZombie()
    {
        if (zombiesToSpawn > 0)
        {
            var spawnPosition = new Vector3(Random.Range(-15, 16), 0, 31);

            //int number = Random.Range(0, 5);
            //if (number == 0)
            //{
            //    Instantiate(zombie, spawnPosition, zombie.transform.rotation);
            //}
            //else
            //{
            //    Instantiate(mindlessZombie, spawnPosition, zombie.transform.rotation); // Quaternion.Euler(transform.rotation * new Vector3(0, 180, 0)));
            //}
            Instantiate(mindlessZombie, spawnPosition, zombie.transform.rotation); // Quaternion.Euler(transform.rotation * new Vector3(0, 180, 0)));

            zombiesToSpawn--;
            Invoke("SpawnZombie", delayTime);
            zombiesAlive++;
        }
    }

    private void SpawnPoPoZombie()
    {
        if (popoZombiesToSpawn > 0 && canSpawnPoPoZombie)
        {
            var spawnPosition = new Vector3(Random.Range(-10, 11), 0, 31);
            Instantiate(popoZombie, spawnPosition, popoZombie.transform.rotation); // Quaternion.Euler(transform.rotation * new Vector3(0, 180, 0)));
            popoZombiesToSpawn--;
            Invoke("SpawnPoPoZombie", delayTime + 4f);
            zombiesAlive++;
        }
    }

    private void SpawnExploder()
    {
        if (explodersToSpawn > 0 && canSpawnExploder)
        {
            var spawnPosition = new Vector3(Random.Range(-15, 16), 0, 31);
            Instantiate(exploder, spawnPosition, exploder.transform.rotation); // Quaternion.Euler(transform.rotation * new Vector3(0, 180, 0)));
            explodersToSpawn--;
            Invoke("SpawnExploder", delayTime + 10f);
            zombiesAlive++;
        }
    }

    public void SpawnNextWave(int waveNumber) // Called from WaveManager
    {
        switch(waveNumber)
        {
            case 2: // if it is wave 5 it will spawn popoZombies
                canSpawnPoPoZombie = true;
                break;
            case 3:
                canSpawnExploder = true;
                break;
            case 4:
                break;
        }
        if (WaveManager.isBossWave == false)
        {
            zombiesToSpawn = waveNumber * 5;
            SpawnZombie();
            if (canSpawnPoPoZombie)
            {
                popoZombiesToSpawn = waveNumber;
                SpawnPoPoZombie();
            }
            if (canSpawnExploder)
            {
                explodersToSpawn = (waveNumber * 2) - Random.Range(2, 5);
                SpawnExploder();
            }
            //Debug.Log("Done Spawning Wave");
        }
        else
        {
            SpawnBoss();
        }
    }

    public void SpawnBoss()
    {
        var spawnPosition = new Vector3(0, 0, 31f);
        Instantiate(boss, spawnPosition, boss.transform.rotation);
        zombiesAlive++;
        //isBossSpawned = true;
    }

    public void SpawnReinforcement(int phase)
    {
        switch(phase){
            default:
                zombiesToSpawn = 15; // Random.Range(10, 26);
                SpawnZombie();
                break;
            case 2:
                zombiesToSpawn = 25; // Random.Range(20, 36);
                popoZombiesToSpawn = 5;
                SpawnPoPoZombie();
                SpawnZombie();
                break;
            //case 3:
            //    zombiesToSpawn = Random.Range(30, 46);
            //    SpawnZombie();
            //    break;
        }
    }
}
