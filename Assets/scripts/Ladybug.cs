using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up*speed);        
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = transform.up * speed;
    }

}
