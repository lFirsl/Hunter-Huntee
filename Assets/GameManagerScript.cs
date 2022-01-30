using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    //The GM script should contain the win condition of the player, allow them to proceed after the win
    //Other important functions/features could be written in here.


    [Tooltip("The win condition game object")]
    public GameObject winGo;
    public Spawner spawner;
    
    public int numOfEnemies = 8;
    public int enemiesKilled;
    public int spawnStartTime;
    public int spawnWait;

    private int maxNumEnemies = 30;
    void Start()
    {
        enemyScript[] enemies = FindObjectsOfType((typeof(enemyScript))) as enemyScript[];
        
        spawner.enemies = enemies;
        spawner.startTime = spawnStartTime;
        spawner.spawnWait = spawnWait;
        spawner.spawnMinWait = 2;
        spawner.spawnMaxWait = 8;
        spawner.numOfEnemies = numOfEnemies;
        
        GameObject playObject = FindObjectOfType<playScript>().gameObject;
        //AVOID SPAWNING TOO CLOSE TO PLAYER
        var position = playObject.transform.position;
        spawner.spawnLimit = new Vector3(position.x + 3f, 0,
                                            position.z + 3f);
        //THIS ONLY SETS THE NUMBER OF ENEMIES AT THE START
        //IF WE INSTANTIATE IN BOSS FIGHTS OR OTHER ENEMY TYPES
        //WE HAVE TO NOTIFY THIS GM GAMEOBJECT
        //THIS WILL BE SEPARATE FROM THE BOSS
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfEnemies > maxNumEnemies)
            spawner.halt = true;
        else
            spawner.halt = false;
        
        if (enemiesKilled == numOfEnemies)
        {
           //Debug.Log("Win condition achieved");
           if (GameObject.Find("WinBox"))
           {
               winGo = GameObject.Find("WinBox");
               winGo.GetComponent<winScript>().winConditionAchieved = true;
           }
           else
           {
               Debug.Log("No win condition present");
           }
        }
    }
    
    
}
