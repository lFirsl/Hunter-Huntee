using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Rifleman : enemyScript
{
    public GameObject bullet;
    public bullet bulletScript;
    public Transform bulletStartPos;
    public float bulletSpeed;
    
    
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
    }

    public override void Attack() //shoot
    {
        base.Attack();
        //Instantiate bullet and stuff now.
        GameObject tmp = Instantiate(bullet);
        bulletScript = tmp.GetComponent<bullet>();
        tmp.transform.position = bulletStartPos.position;
        bulletScript.activate = true;
        bulletScript.speed = bulletSpeed;
    }
}
