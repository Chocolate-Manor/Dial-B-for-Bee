using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SharpDamage : MonoBehaviour
{
    private bool hasHit;

    [SerializeField] private AudioClip knifeHitSound;
    
    // Start is called before the first frame update
    void Start()
    {
        hasHit = false;
    }

    private void OnEnable()
    {
        Butterfly.OnCollision += OnHitBehavior;
        Butterfly.OnCollision += OnHitDamage; 
    }

    private void OnDisable()
    {
        Butterfly.OnCollision -= OnHitBehavior;
        Butterfly.OnCollision -= OnHitDamage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasHit == false)
        {
            hasHit = true;
            OnHitBehavior(other);
            OnHitDamage(other);
        }
    }
    
    public void OnHitBehavior(Collision2D other)
    {
        //make it stop moving
        Destroy(GetComponentInParent<Rigidbody2D>());
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
