using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //The GM script should contain the win condition of the player, allow them to proceed after the win
    //Other important functions/features could be written in here.


    [Tooltip("The win condition game object")]
    public GameObject winGo;
    
    public int numOfEnemies;
    public int enemiesKilled;
    
    void Start()
    {
        
        enemyScript[] enemies = FindObjectsOfType((typeof(enemyScript))) as enemyScript[];
        foreach (enemyScript enemy in enemies)
        {
            Debug.Log(enemy.gameObject.name);
            numOfEnemies += 1;
        }

        //THIS ONLY SETS THE NUMBER OF ENEMIES AT THE START
        //IF WE INSTANTIATE IN BOSS FIGHTS OR OTHER ENEMY TYPES
        //WE HAVE TO NOTIFY THIS GM GAMEOBJECT
        //THIS WILL BE SEPARATE FROM THE BOSS
    }

    // Update is called once per frame
    void Update()
    {
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
