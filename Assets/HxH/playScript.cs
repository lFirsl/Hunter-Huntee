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
    public GameObject character;
    public float maxHealth = 100f;
    public float currentHealth;
    public Animator rabAnim;
    public bool aggressive;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        aggressive = false;
        rabAnim = rab.GetComponent<Animator>();
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

        FindObjectOfType<audioManager>().Play("test");
    }

    void Motion()
    {
        // rb.velocity = ((Input.GetAxis("Vertical") * transform.forward * Time.deltaTime) +
        //                (Input.GetAxis("Horizontal") * transform.right * Time.deltaTime)).normalized * speed;
        
        //Get new force additions
        Vector3 verticalSpeed = transform.forward * speed * Input.GetAxis("Vertical");
        Vector3 horizontalSpeed = transform.right * speed * Input.GetAxis("Horizontal");
        //add force
        rb.AddForce(verticalSpeed);
        rb.AddForce(horizontalSpeed);

        Vector3 movement = rb.velocity;
        
        //check whether to turn run animation on or off.
        if (movement.magnitude > 0.01)
        {
            rabAnim.SetTrigger("runTrigger");
            
            //Correct rotation
            character.transform.rotation =
                Quaternion.LookRotation(new Vector3(movement.x, 0.0f, movement.z) * Time.deltaTime);
        }
        else rabAnim.ResetTrigger("runTrigger");
    }

    void setAnimation()
    {
        rabAnim.SetTrigger("runTrigger");
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;
    }
}
