using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lifeSpan = 5;
    [SerializeField] private float speed;

    private bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.right * speed);
        //Destroy(gameObject, lifeSpan);
        hasHit = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnHitBehavior(other);
        OnHitDamage(other);
        
    }

    public void OnHitBehavior(Collision2D other)
    {
        if (hasHit == false)
        {
            hasHit = true;
            //make it stop moving on hit.
            Destroy(rb);
            //make it stick
            transform.parent = other.transform;
        } 
    }
    
    public void OnHitDamage(Collision2D other)
    {
        IDamagable enemy = other.gameObject.GetComponentInChildren<IDamagable>();
        if (enemy != null)
        {
            enemy.Damage();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}