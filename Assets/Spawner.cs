using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public enemyScript[] enemies;
    public Vector3 spawnLimit;
    
    public int numOfEnemies = 0;
    public float spawnWait;
    public float spawnMinWait;
    public float spawnMaxWait;
    public int startTime;

    public bool halt;
    
    void Start()
    {
        foreach (enemyScript enemy in enemies)
        {
            Debug.Log(enemy.gameObject.name);
        }

        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spawnMinWait, spawnMaxWait);
    }


    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startTime);
        while(!halt)
        {
            int randEnemy = Random.Range(0, enemies.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnLimit.x, spawnLimit.x),
                                            Random.Range(-spawnLimit.z, spawnLimit.z));
            Instantiate(enemies[randEnemy], spawnPos + transform.TransformPoint(0, 0, 0),
                        gameObject.transform.rotation);
            numOfEnemies++;
            Debug.Log(enemies[randEnemy].gameObject.name);
        }

        yield return new WaitForSeconds(spawnWait);
    }
}

