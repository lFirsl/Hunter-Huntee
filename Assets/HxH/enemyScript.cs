using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class enemyScript : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public float attackInterval;
    public float damage = 20f;
    private float currentCooldown;
    
    public playScript playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = attackInterval;
        playScript playerScript = GameObject.Find("Player").GetComponent<playScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown <= 0)
        {
            Attack();
            currentCooldown = attackInterval;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
        
        
        
    }

    public virtual void Attack()
    {
        currentCooldown = attackInterval;
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<playScript>() != null)
        {
            Debug.Log("collides");
            coll.gameObject.GetComponent<playScript>().ChangeHealth(damage);
        }
    }
}
