using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   
        //make it stop moving on hit.
        Destroy(rb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
