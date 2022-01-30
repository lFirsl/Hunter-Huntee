using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playScript : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public GameObject wolf;
    public GameObject rab;
    public GameObject character;
    public GameObject hitBoxObject;
    private BoxCollider hitBox;
    
    [Header("Numbers and State")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float speed;
    public bool aggressive;
    [Tooltip("Current sanity points")]
    public float sanityVar = 0;
    [Tooltip("Sanity increment/Increment per second")] 
    public float sanityInc;
    [Tooltip("The number of sanity points gained per second")]
    public float sanityCD;
    public float currentSanityCD;
    
    
    [Header("SFX and ANIM")]
    private Animator rabAnim;
    private Animator wolfAnim;
    public bool walkSound;
    
    public static playScript Instance;



    private float temp_cooldown;
    
    //Start is called before the first frame update
    void Start()
    {
        currentSanityCD = sanityCD;
        aggressive = false;
        walkSound = false;
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
        SanityCheck();
        Debug.Log(sanityVar);
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

    void SanityCheck() //Increments/Decrements sanity bar
    {
        if (aggressive)
        {
            if (currentSanityCD <= 0)
            {
                sanityVar += sanityInc;
                currentSanityCD = sanityCD;
            }
            else
            {
                currentSanityCD -= Time.deltaTime;
            }
        }
        else
        {
            if (currentSanityCD <= 0)
            {
                sanityVar -= sanityInc;
                currentSanityCD = sanityCD;
            }
            else
            {
                currentSanityCD -= Time.deltaTime;
            }
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

            //play walking sound here
            if(!walkSound){
                StartCoroutine(startWalkSound());
                FindObjectOfType<audioManager>().Play("walk");  
            }  
        }
        else
        {
            if (aggressive) wolfAnim.SetBool("runTrigger",false);
            else rabAnim.ResetTrigger("runTrigger");
        }

    }

    IEnumerator startWalkSound(){
        walkSound = true;
        yield return new WaitForSeconds(0.45f);
        walkSound = false;
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


            //sound system below
            //random number
            System.Random rand = new System.Random();
            int hold = rand.Next(1,3);

            //will randomly switch between two attacking sound effects
            if(hold == 1){
                FindObjectOfType<audioManager>().Play("wolfAttack1");
            }else{
                FindObjectOfType<audioManager>().Play("wolfAttack2");
            }
            //add wolf attack sound here
            
        }
        //wolfAnim.SetBool("AtkVer",!wolfAnim.GetBool("AtkVer"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        //throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("projectile"))
        {
            //whoever is working on the HP system, do your thing
            Debug.Log("Player is hit!");
        }
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
