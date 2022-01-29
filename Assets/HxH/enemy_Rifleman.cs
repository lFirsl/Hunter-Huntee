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
            StartCoroutine(shoot());
            GameObject tmp = Instantiate(bullet);
            bulletScript = tmp.GetComponent<bullet>();
            tmp.transform.position = bulletStartPos.position;
            bulletScript.activate = true;
            bulletScript.speed = bulletSpeed; 
        }
    }

    IEnumerator shoot()
    {
        shot = true;
        yield return new WaitForSeconds(shotCD);
        shot = false;
    }
    public override void Active()
    {
        WalkTowardsPlayer();
    }
    
    void WalkTowardsPlayer()
    {
        Debug.Log("Walking");
        if (currentDistanceFromPlayer >= attDist)
        {
            rb.AddForce(transform.forward * movementSpeed * aggroModifier);
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
        if (currentDistanceFromPlayer >= kiteDist)
        {
            //Need to use velocity instead else ignores colliders and everything lol
            rb.AddForce(transform.forward * movementSpeed * Time.deltaTime);
        }

        if (currentDistanceFromPlayer < kiteDist)
        {
            rb.AddForce(transform.forward * -movementSpeed * Time.deltaTime);
        }
    }
}
