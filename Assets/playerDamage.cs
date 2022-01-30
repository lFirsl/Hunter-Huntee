using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    public float playerdmg;
    
    // Start is called before the first frame update
    void Start()
    {
        playerdmg = transform.root.GetComponent<playScript>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<bossScript>().TakeDamage(playerdmg);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<enemyScript>().TakeDamage(playerdmg);
        }
    }
}
