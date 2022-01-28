using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playScript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject wolf;
    public GameObject rab;
    public float maxHealth = 100f;
    public float currentHealth;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        wolf.SetActive(true);
        rab.SetActive(false);
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        Motion();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Switch();
            //Debug.Log("works");
        }
    }


    void Switch()
    {
        if (wolf.activeSelf)
        {
            RabbitChange();
        }
        else
        {
            WolfChange();
        }
    }

    void WolfChange()
    {
        rab.SetActive(false);
        wolf.SetActive(true);
    }

    void RabbitChange()
    {
        rab.SetActive(true);
        wolf.SetActive(false);
    }
    void Motion()
    {
<<<<<<< HEAD
        // rb.velocity = ((Input.GetAxis("Vertical") * transform.forward * Time.deltaTime) +
        //                (Input.GetAxis("Horizontal") * transform.right * Time.deltaTime)).normalized * speed;

        rb.AddForce(transform.forward * speed * Input.GetAxis("Vertical"));
        rb.AddForce(transform.right * speed * Input.GetAxis("Horizontal"));
=======
        rb.velocity = ((Input.GetAxis("Vertical") * transform.forward * Time.deltaTime) +
                       (Input.GetAxis("Horizontal") * transform.right * Time.deltaTime)).normalized * speed;
>>>>>>> 0dbc8be (Delete and Ingore Library folder)
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;
    }
}
