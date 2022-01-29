using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class winScript : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider bc;
    
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        bc.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<playScript>())
        {
            Debug.Log("COLLIDED");
            GameObject ob = GameObject.Find("LevelLoader");
            ob.GetComponent<LevelLoaderScript>().LoadNextLevel();
        }
    }
}
