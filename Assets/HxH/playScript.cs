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
    public float sanityLimit;
    [Tooltip("The upper and lower limit of sanity points")]
    public float sanityCD;
    public float currentSanityCD;
    public bool canTransform;
    public float transformationCD = 4;
    private float doubletransformationCD;
    private bool dead;

    public float damage;
    
    
    [Header("SFX and ANIM")]
    private Animator rabAnim;
    private Animator wolfAnim;
    public bool attacking;
    public bool walkSound;
    
    [Header("Others")]
    public GameObject hitBoxObject;
    private BoxCollider hitBox;
    
    public static playScript Instance;



    private float temp_cooldown;
    
    //Start is called before the first frame update
    void Start()
    {
        currentSanityCD = sanityCD;
        aggressive = true;
        walkSound = false;
        attacking = false;
        canTransform = true;
        dead = false;
        doubletransformationCD = 2 * transformationCD;
        rabAnim = rab.GetComponent<Animator>();
        wolfAnim = wolf.GetComponent<Animator>();
        hitBox = hitBoxObject.GetComponent<BoxCollider>();
        hitBox.enabled = false;
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
        if (!dead)
        {
            Motion();
            SanityCheck();
            //Debug.Log(sanityVar);
            if (Input.GetKeyUp(KeyCode.E) && canTransform)
            {
                SwitchForm();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                attack();
            }
        }
        if (currentHealth <= 0)
        {
            dead = true;
            wolfAnim.SetTrigger("death");
            rabAnim.SetTrigger("death");
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

        if (sanityVar > sanityLimit)
        {
            sanityVar -= 20;
            transformationCD = doubletransformationCD;
            SwitchForm();
        }
        else if (sanityVar < -sanityLimit)
        {
            sanityVar += 20;
            transformationCD = doubletransformationCD;
            SwitchForm();
        }
    }
    
    

    void SwitchForm()
    {
        aggressive = !aggressive; //switch from Aggressive to Passive, or vice-versa
        //if aggressive is true, turn to wolf. Otherwise, turn to rabbit.
        wolf.SetActive(aggressive);
        rab.SetActive(!aggressive);

        if(aggressive)FindObjectOfType<audioManager>().Play("intoWolf");
        else if(!aggressive) FindObjectOfType<audioManager>().Play("intoRab");

        StartCoroutine(startCD());
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

    IEnumerator startCD()
    {
        canTransform = false;
        yield return new WaitForSeconds(transformationCD);
        if (transformationCD >= doubletransformationCD) transformationCD = doubletransformationCD / 2;
        canTransform = true;
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
        Debug.Log("Player was hit!");
        if (other.CompareTag("projectile"))
        {
            // Debug.Log("Spikemen collided with Player");
            // if(!aggressive || attacking) ps.ChangeHealth(spikeDmg);
        }
        else if (other.CompareTag("pike"))
        {
            float damage = 10;
            if (!aggressive) ChangeHealth(damage/2);
            else if(!attacking) ChangeHealth(damage);
            
        }
    }

    IEnumerator activateHitbox()
    {
        hitBox.enabled = true;
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
        hitBox.enabled = false;
    }

    //Health is done by calling this funciton in other scripts
    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;
    }
}
