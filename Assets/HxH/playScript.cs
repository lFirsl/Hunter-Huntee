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
    private float maxHealth = 100f;
    public float currentHealth;
    private Animator rabAnim;
    private Animator wolfAnim;
    public bool aggressive;
    
    public static playScript Instance;
    public GameObject hitBoxObject;
    private BoxCollider hitBox;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        aggressive = false;
        rabAnim = rab.GetComponent<Animator>();
        wolfAnim = wolf.GetComponent<Animator>();
        hitBox = hitBoxObject.GetComponent<BoxCollider>();
        hitBox.enabled = false;
        SwitchForm();
        currentHealth = maxHealth;
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
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
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attack();
      }
        
    }


    void SwitchForm()
    {
        aggressive = !aggressive; //switch from Aggressive to Passive, or vice-versa
        //if aggressive is true, turn to wolf. Otherwise, turn to rabbit.
        wolf.SetActive(aggressive);
        rab.SetActive(!aggressive);

        if(aggressive)FindObjectOfType<audioManager>().Play("intoWolf");
        else FindObjectOfType<audioManager>().Play("intoRab");

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
            if (aggressive) wolfAnim.SetBool("runTrigger",true);
            else rabAnim.SetTrigger("runTrigger");
            
            //Correct rotation
            character.transform.rotation =
                Quaternion.LookRotation(new Vector3(movement.x, 0.0f, movement.z) * Time.deltaTime);
        }
        else
        {
            if (aggressive) wolfAnim.SetBool("runTrigger",false);
            else rabAnim.ResetTrigger("runTrigger");
        }
    }

    void setAnimation()
    {
        rabAnim.SetTrigger("runTrigger");
    }

    void attack()
    {
        if (aggressive)
        {
            StartCoroutine(activateHitbox());
            wolfAnim.SetTrigger("Attack");
        }
        //wolfAnim.SetBool("AtkVer",!wolfAnim.GetBool("AtkVer"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        throw new NotImplementedException();
    }

    IEnumerator activateHitbox()
    {
        hitBox.enabled = true;
        yield return new WaitForSeconds(1);
        hitBox.enabled = false;
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;
    }
}
