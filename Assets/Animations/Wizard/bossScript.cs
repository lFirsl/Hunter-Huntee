using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class bossScript : MonoBehaviour
{
    [Header("ATTACK NUMBERS")] public float basicAtt;
    public float sweepAtt;
    public float dist;
    
    [Header("Refernces")]
    public GameObject player;
    public float constAttTime;
    public int numOfSweeps;
    public bool invulnerable = false;
    public float invulnerableTime;
    public float spawningTime;
    public Transform attackPoint;
    public float circleRadius;
    public Transform sweepPoint;
    public float sweepRad;
    public float s2Rad;
    public float s2dmg;
    


    public float maxhealth = 500f;
    public float currenthealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxhealth;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardsPlayer();
        dist = Vector3.Distance(player.transform.position, transform.position);
    }

    public void TakeDamage(float dmg)
    {
        if (invulnerable)
        {
            return;
        }
        if (currenthealth <= 250)
        {
            GetComponent<Animator>().SetBool("isStage2", true);
        }

        if (currenthealth <= 0)
        {
            Destroy(this);
        }
        
        currenthealth -= dmg;
    }

    public void LookTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction = new Vector3(direction.x, 0.0f, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = rotation;
    }

    public int DecidePattern()
    {
        return Random.Range(0, 2);
    }

    public int DecidePatternS2()
    {
        return Random.Range(0, 5);
    }

    public void Maintain()
    {
        //Activate Coroutine;
        StartCoroutine(ConstantAtt());
    }

    IEnumerator ConstantAtt()
    {
        yield return new WaitForSeconds(constAttTime);
        this.GetComponent<Animator>().SetTrigger("ConstAttack");
    }

    public void Sweep() //This is to fix the problem where you dont go back to the decider state
    {
        StartCoroutine(MultiSweep());
    }

    IEnumerator MultiSweep()
    {
        yield return new WaitForSeconds(1.33f * numOfSweeps);
        GetComponent<Animator>().SetTrigger("SweepAttack");
    }

    public void callInvul()
    {
        StartCoroutine(InvulnerableTime());
    }

    IEnumerator InvulnerableTime()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
        GetComponent<Animator>().SetTrigger("InvulEnds");
    }

    public void BasicAttack()
    {
        Debug.Log("BasicAttack Executed");
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, circleRadius);
        foreach (var hitColliders in colInfo)
        {
            if (hitColliders.GetComponent<playScript>())
            {
                playScript ps = hitColliders.GetComponent<playScript>();
                ps.ChangeHealth(basicAtt);
                Debug.Log("Basic");
            }
        }
    }

    public void SweepAttack()
    {
        Collider[] colInfo = Physics.OverlapSphere(attackPoint.position, sweepRad);
        foreach (var hitColliders in colInfo)
        {
            if (hitColliders.GetComponent<playScript>())
            {
                playScript ps = hitColliders.GetComponent<playScript>();
                ps.ChangeHealth(sweepAtt);
                Debug.Log("Sweep");
            }
        }
    }

    public void Stage2Attack()
    {
        Collider[] colInfo = Physics.OverlapSphere(transform.position, s2Rad);
        foreach (var hitColliders in colInfo)
        {
            if (hitColliders.GetComponent<playScript>())
            {
                playScript ps = hitColliders.GetComponent<playScript>();
                ps.ChangeHealth(s2dmg);
                Debug.Log("S2");
            }
        }
    } 
    
    
    
    public void callEnemies()
    {
        StartCoroutine(callEnemiesCoroutine());
    }

    IEnumerator callEnemiesCoroutine()
    {
        yield return new WaitForSeconds(spawningTime);
        GetComponent<Animator>().SetTrigger("SpawnEnemiesEnd");
    }

    
}
