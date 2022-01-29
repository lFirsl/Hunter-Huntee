using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public BoxCollider bc;
    public bool activate; //Used to activate the Shoot command when called in rifleman script
    // Start is called before the first frame update
    void Start()
    {
        activate = false;
        rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        rb.angularDrag = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            rb.velocity = transform.up * speed;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //code for destroying and anim.
    }
}
