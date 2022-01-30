using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody rb;
    public BoxCollider bc;
    public bool activate; //Used to activate the Shoot command when called in rifleman script

    public float bulletdmg = 5f; //bullet dmg
    private float destructTimer = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        activate = false;
        rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        rb.angularDrag = 0f;
        StartCoroutine(selfDestruct());

        //Find and look towards player
        GameObject target = GameObject.Find("Player");
        Vector3 playerPosition = target.transform.position;
        Vector3 direction = playerPosition - transform.position;
        
        direction = new Vector3(direction.x, 0.0f, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = rotation;
        
        //Shoot in the correct direction
        rb.AddForce(transform.forward * speed * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // if (activate)
        // {
        //     rb.velocity = transform.up * speed;
        // }
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(destructTimer);
        Destroy(gameObject);
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet collision");
        if (other.CompareTag("Player"))
        {
            GameObject parent = other.transform.root.gameObject;
            playScript ps = parent.GetComponent<playScript>();
            ps.ChangeHealth(bulletdmg);
            Debug.Log("Collided with Player");
            Destroy(gameObject);
        }
        //code for destroying and anim.
    }
}
