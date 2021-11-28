using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : Bug
{
    [SerializeField] private float lifeSpan = 500;
    // Destroy(gameObject, lifeSpan);

    private float initializationTime;

    private Explosion explosion;
    [SerializeField] private GameObject exploder;
    
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        this.name = "Ladybug";
        initializationTime = Time.timeSinceLevelLoad;
        rb.AddForce(transform.up * speed);
        
        //explode it after lifespan. 
        Instantiate(exploder).GetComponent<Explosion>().Explode(lifeSpan, gameObject);
        Destroy(gameObject, lifeSpan + 0.1f);
    } 
}