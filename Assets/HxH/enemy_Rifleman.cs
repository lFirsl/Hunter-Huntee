using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class enemy_Rifleman : enemyScript
{
    public GameObject bullet;
    public bullet bulletScript;
    public Transform bulletStartPos;
    public float bulletSpeed;
    public float kiteDist = 30;

    public float shotCD = 1.0f;
    private bool shot = false;


    // BULLETS
    private int mag = 3;
    
    public enemy_Rifleman()
    {
        base.damage = 20f;
        base.health = 100f;
        base.attackInterval = 20f;
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();     
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        LookTowardsPlayer();
        //WalkTowardsPlayer();
    }

    public override void Attack() //shoot
    {
        base.Attack();
        //Instantiate bullet and stuff now.
        if (!shot)
        {
            if (mag > 0)
            {
                StartCoroutine(shoot());
                mag--;
                GameObject tmp = Instantiate(bullet);
                bulletScript = tmp.GetComponent<bullet>();
                bulletScript.speed = bulletSpeed;
                tmp.transform.position = bulletStartPos.position;
                bulletScript.activate = true;

                //sound of rifle being fired
                FindObjectOfType<audioManager>().Play("bangGun");

            }
            else
            {
                StartCoroutine(reload());
                mag = 3;
            }
        }
    }

    IEnumerator shoot()
    {
        shot = true;
        yield return new WaitForSeconds(shotCD);
        shot = false;
    }

    IEnumerator reload()
    {
        shot = true;
        yield return new WaitForSeconds(3); //time needed to reload
        shot = false;
    }
    public override void Active()
    {
        WalkTowardsPlayer();
    }
    
    void WalkTowardsPlayer()
    {
        Debug.Log("Walking");
        if (currentDistanceFromPlayer > aggroDistance) return;
        else if (currentDistanceFromPlayer >= attDist)
        {
            //rb.AddForce(transform.forward * movementSpeed * aggroModifier);
            rb.MovePosition(transform.position + transform.forward * movementSpeed * aggroModifier * Time.deltaTime);
            //transform.position += transform.forward * movementSpeed * aggroModifier * Time.deltaTime;
        }
    }

    public override void Passive()
    {
        RunAwayfromPlayer();
    }
    
    void RunAwayfromPlayer()
    {
        //move such that the distance is equal to kiteDist
        if (currentDistanceFromPlayer > aggroDistance) return;
        else if (currentDistanceFromPlayer >= kiteDist)
        {
            //Need to use velocity instead else ignores colliders and everything lol
            //rb.AddForce(transform.forward * movementSpeed * Time.deltaTime);
            //transform.position += transform.forward * movementSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + transform.forward * movementSpeed *  Time.deltaTime);
        }

        else if (currentDistanceFromPlayer < kiteDist)
        {
            //rb.AddForce(transform.forward * -movementSpeed * Time.deltaTime);
            //transform.position += -transform.forward * movementSpeed * Time.deltaTime;
            rb.MovePosition(transform.position - transform.forward * movementSpeed * Time.deltaTime);
        }


    }
}
