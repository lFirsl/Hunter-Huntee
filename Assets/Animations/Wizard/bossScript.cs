using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public GameObject player;


    public float health = 500f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        if (health <= 250)
        {
            GetComponent<Animator>().SetBool("isStage2", true);
        }
    }

    public void LookTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction = new Vector3(direction.x, 0.0f, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = rotation;
    }

    public void BasicAttack()
    {
        Debug.Log("BasicAttack Executed");
    }
}
