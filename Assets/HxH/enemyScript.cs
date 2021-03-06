using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class enemyScript : MonoBehaviour
{
    [Header("Statistics")]
    public float health;

    public float currentHealth;
    public float movementSpeed;
    public float attackInterval;
    public float damage = 20f;
    public float attDist = 4f;
    public float aggroModifier = 1.5f; //Speed modifier when enemies are in aggro state. 
    public float aggroDistance = 20f;
    
    [Header("Internal Variables")]
    private float rotProgress = -1f; //Keeps track of rotation progress.

    public bool dead;
    
    [Header("References")]
    private float currentCooldown;
    public Vector3 playerPosition;
    public float currentDistanceFromPlayer;
    public GameObject player;
    public playScript playerScript;
    protected Rigidbody rb;
    
    [Header("Booleans")]
    public bool isAttacking = false;
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        dead = false;
        isAttacking = false;
        currentCooldown = attackInterval;
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<playScript>();
        rb = GetComponent<Rigidbody>();
        currentHealth = health;
    }

    // Update is called once per frame
    protected void Update()
    {
        playerPosition = player.transform.position;
        currentDistanceFromPlayer = Vector3.Distance(transform.position, playerPosition);
        
        AttackSys(); //Handles the att coroutine and attack cooldowns. 
        if (health <= 0) 
        {
            Die();
        }
        
        if (playerScript.aggressive && !isAttacking) //Makes it such that enemy cant move when attacking and only move after
        {
            Passive();
        }
        else if (!playerScript.aggressive && !isAttacking)
        {
            Active();
        }
    }

    public virtual void AttackSys()
    {
        if (currentDistanceFromPlayer <= attDist)
        {
            isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    protected void LookTowardsPlayer()
    {
        Vector3 direction = playerPosition - transform.position;
        direction = new Vector3(direction.x, 0.0f, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        //transform.rotation = rotation;
        this.transform.rotation = rotation;
    }

    public void TakeDamage(float dmg)
    {
        // currentHealth -= dmg;
        Destroy(gameObject);
    }
    
    
    IEnumerator AttackCoroutine()
    {
        //Debug.Log(isAttacking);
        //ANIM HERE
        //SFX HERE
        Attack();
        isAttacking = false;
        yield return new WaitForSeconds(5f); //Enter time finish for att anim
        
        //Debug.Log(isAttacking);
    }

    public virtual void Attack() //This function can be over written for specific attacks
    {
        //Attack sound here
    }

    public virtual void Active()
    {
        //Override Aggro Behaviour here
    }

    public virtual void Passive()
    {
        //Override Passive Behaviour here
    }

    public void Die()
    {
        //Play Sfx
        //Play DeathAnimation
    }
    
    

    /*private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<playScript>() != null)
        {
            Debug.Log("collides");
            coll.gameObject.GetComponent<playScript>().ChangeHealth(damage);
        }
    }*/
}
