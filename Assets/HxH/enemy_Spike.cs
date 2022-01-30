using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Spike : enemyScript
{
    private float kiteDist = 15f;
    public float spikeDmg = 10f;
        
    public enemy_Spike()
    {
        base.damage = 20f;
        base.health = 100f;
        base.attackInterval = 20f;
    }
    
    void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        base.Update();
        LookTowardsPlayer();
    }
    

    public override void Active()
    {
        WalkTowardsPlayer();
    }

    public override void Passive()
    {
        RunAwayfromPlayer();
    }

    

    void WalkTowardsPlayer()
    {
        if (currentDistanceFromPlayer >= attDist)
        {
            transform.position += transform.forward * movementSpeed * aggroModifier * Time.deltaTime;
        }
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collision");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spikemen collided with Player");
            GameObject parent = other.transform.root.gameObject;
            playScript ps = parent.GetComponent<playScript>();
            if(!ps.aggressive || !ps.attacking) ps.ChangeHealth(spikeDmg);
        }
    }
}
