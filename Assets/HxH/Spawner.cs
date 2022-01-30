using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3 spawnLimit;
    
    public float spawnWait;
    public float spawnMinWait;
    public float spawnMaxWait;
    public int startTime;
    
    public int enemiesSpawned;
    public int maxEnemies;
    
    public bool halt;
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
        enemiesSpawned++;
    }


    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startTime);
        while (!halt && maxEnemies < enemiesSpawned)
        {
            int randEnemy = Random.Range(0, enemies.Length);
            
            Vector3 spawnPos = new Vector3(Random.Range(-spawnLimit.x, spawnLimit.x),
                                            Random.Range(-spawnLimit.z, spawnLimit.z));
            
            Instantiate(enemies[randEnemy], spawnPos + transform.TransformPoint(0, 0, 0),
                            gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}

