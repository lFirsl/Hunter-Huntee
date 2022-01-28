using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public float damage = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
