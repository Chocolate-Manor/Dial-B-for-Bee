using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 explosionPos;
    [SerializeField] private CircleCollider2D explosionArea;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private GameObject light;
    [SerializeField] private Animator ani;
    private void Start()
    {
        explosionPos = transform.position;
    }

    public void Explode()
    {
        ani.SetTrigger("Explode");
        explosionParticle.Play();
        Collider2D[] collider2Ds =  Physics2D.OverlapCircleAll(explosionPos, explosionArea.radius);
        foreach (Collider2D other in collider2Ds)
        {
            IDamagable enemy = other.GetComponentInChildren<IDamagable>();
            if (enemy != null)
            {
                enemy.Damage();
            }
        }
    }

    IEnumerator ExplodeAfterTime(float time, GameObject obj)
    {
        transform.position = obj.transform.position;
        yield return new WaitForSeconds(time);
        Explode();
        Destroy(obj);
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
    
    /// <summary>
    /// Overload method to explode after a certain time. 
    /// </summary>
    /// <param name="time"></param>
    public void Explode(float time, GameObject obj)
    {
        StartCoroutine(ExplodeAfterTime(time, obj));
    }
}
