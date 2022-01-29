using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playScript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject wolf;
    public GameObject rab;
    public float maxHealth = 100f;
    public float currentHealth;
    private bool aggressive;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        aggressive = false;
        SwitchForm();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        Motion();
        if (Input.GetKeyUp(KeyCode.E))
        {
            SwitchForm();
            //Debug.Log("works");
        }
    }


    void SwitchForm()
    {
        aggressive = !aggressive; //switch from Aggressive to Passive, or vice-versa
        //if aggressive is true, turn to wolf. Otherwise, turn to rabbit.
        wolf.SetActive(aggressive);
        rab.SetActive(!aggressive);
    }
    void Motion()
    {
        // rb.velocity = ((Input.GetAxis("Vertical") * transform.forward * Time.deltaTime) +
        //                (Input.GetAxis("Horizontal") * transform.right * Time.deltaTime)).normalized * speed;

        rb.AddForce(transform.forward * speed * Input.GetAxis("Vertical"));
        rb.AddForce(transform.right * speed * Input.GetAxis("Horizontal"));
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;
    }
}
