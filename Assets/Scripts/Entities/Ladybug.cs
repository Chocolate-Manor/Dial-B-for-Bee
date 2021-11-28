using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 500;
    // Destroy(gameObject, lifeSpan);

    private float initializationTime;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
        rb.AddForce(transform.up * speed);
        Destroy(gameObject, lifeSpan);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}