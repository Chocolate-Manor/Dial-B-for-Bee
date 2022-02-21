using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SharpDamage : MonoBehaviour, IProjectile
{
    private bool hasHit;
    private CollisionObserver co;
    
    [SerializeField] private AudioClip knifeHitSound;
    
    // Start is called before the first frame update
    void Start()
    {
        hasHit = false;
    }

    private void OnEnable()
    {
        co = GetComponentInParent<CollisionObserver>();
        co.OnCollision += OnHitBehavior;
        co.OnCollision += OnHitDamage; 
    }

    private void OnDisable()
    {
        co.OnCollision -= OnHitBehavior;
        co.OnCollision -= OnHitDamage;
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
