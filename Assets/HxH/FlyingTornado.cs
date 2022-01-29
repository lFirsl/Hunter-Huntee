using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingTornado : MonoBehaviour
{
    public float flyingSpeed;
    public float windSpeed;
    public bool activate;
    public WindZone wind;
    public ParticleSystem particle;
    public BoxCollider bc;
    public Rigidbody rb;
    private float destructTimer = 3.0f;
    void Start()
    {
        activate = false;
        wind = gameObject.GetComponent<WindZone>();
        particle = gameObject.GetComponent<ParticleSystem>();
        bc = gameObject.GetComponent<BoxCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        
        bc.isTrigger = true;
        wind.mode = WindZoneMode.Spherical;
        rb.AddForce(0, 0, windSpeed, ForceMode.Impulse);
        var em = particle.emission;
        em.enabled = true;
        em.rateOverTime = 45.0f;
    }

    void Update()
    {
        if (activate)
        {
            rb.GetComponent<Rigidbody> ().velocity = transform.up * flyingSpeed;
        }
        StartCoroutine(selfDestruct());
    }
    
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(destructTimer);
        Destroy(gameObject);
    }
}


