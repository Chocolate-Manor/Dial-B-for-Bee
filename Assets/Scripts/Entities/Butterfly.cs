using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : Bug, IProjectile
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip knifeHitSound;

    private bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        this.name = "Butterfly";
        rb.AddForce(transform.right * speed);
        //Destroy(gameObject, lifeSpan);
        hasHit = (speed == 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasHit == false)
        {
            hasHit = true;
            OnHitBehavior(other);
            OnHitDamage(other);
        }
        
        if (other.gameObject.tag == "Player")
        {
            B b = other.gameObject.GetComponent<B>();
            PickMeUp(b);
            Destroy(gameObject);
        }
    }

    public void OnHitBehavior(Collision2D other)
    {
        //make it stop moving
        Destroy(rb);
        //make it stick
        transform.parent = other.transform;
        //play sound
        GameManager.Instance.PlaySoundEffect(knifeHitSound);
    }

    public void OnHitDamage(Collision2D other)
    {
        IDamagable enemy = other.gameObject.GetComponentInChildren<IDamagable>();
        if (enemy != null)
        {
            enemy.Damage();
        }
    }
}