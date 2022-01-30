using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityBarScript : MonoBehaviour
{
    public Transform pivot;
    public playScript playerScript;
    private float tmp;
    private float tmp_rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<playScript>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp_rotation = 0.55f * playerScript.sanityVar;
        //transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis());
    }
}
