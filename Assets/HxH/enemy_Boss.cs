using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class enemy_Boss : enemyScript
{
    public float kiteDist = 30;
    public GameObject bullet;
    public bullet particle;
    public Transform particleStartPos;
    public float particleSpeed;
    
    
    public enemy_Boss()
    {
        base.damage = 20f;
        base.health = 150f;
        base.attackInterval = 10f;
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
        if (particleSpeed != 0)
            particleSpeed--;
    }

    public override void Attack() //shoot
    {
        base.Attack();
        //Instantiate bullet and stuff now.
        GameObject tmp = Instantiate(bullet);
        particle = tmp.GetComponent<bullet>();
        tmp.transform.position = particleStartPos.position;
        particle.activate = true;
        particle.speed = particleSpeed;
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