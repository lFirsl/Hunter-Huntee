using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class enemy_Rifleman : enemyScript
{
    public GameObject bullet;
    public bullet bulletScript;
    public Transform bulletStartPos;
    public float bulletSpeed;
    public float kiteDist = 30;
    
    
    // BULLETS
    
    public enemy_Rifleman()
    {
        base.damage = 20f;
        base.health = 100f;
        base.attackInterval = 20f;
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();     
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        LookTowardsPlayer();
        
    }

    public override void Attack() //shoot
    {
        base.Attack();
        //Instantiate bullet and stuff now.
        GameObject tmp = Instantiate(bullet);
        bulletScript = tmp.GetComponent<bullet>();
        tmp.transform.position = bulletStartPos.position;
        bulletScript.activate = true;
        bulletScript.speed = bulletSpeed;
    }
    public override void Active()
    {
        WalkTowardsPlayer();
    }
    
    void WalkTowardsPlayer()
    {
        if (currentDistanceFromPlayer >= attDist)
        {
            transform.position += transform.forward * movementSpeed * aggroModifier * Time.deltaTime;
        }
    }

    public override void Passive()
    {
        RunAwayfromPlayer();
    }
    
    void RunAwayfromPlayer()
    {
        //move such that the distance is equal to kiteDist
        if (currentDistanceFromPlayer >= kiteDist)
        {
            //Need to use velocity instead else ignores colliders and everything lol
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }

        if (currentDistanceFromPlayer < kiteDist)
        {
            transform.position += transform.forward * -movementSpeed * Time.deltaTime;
        }
    }
}
