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
    [SerializeField] private AudioClip explosionSound;
    
    private void Start()
    {
        //Destroy itself after a set number of seconds
        Destroy(gameObject, 15);
    }
    
    /// <summary>
    /// Find all enemies in the circle
    /// </summary>
    public void Explode()
    {
        ani.SetTrigger("Explode");
        explosionParticle.Play();
        Collider2D[] collider2Ds =  Physics2D.OverlapCircleAll(transform.position, explosionArea.radius);
        foreach (Collider2D other in collider2Ds)
        {
            IDamagable enemy = other.GetComponentInChildren<IDamagable>();
            if (enemy != null)
            {
                enemy.Damage();
            }
        }
    }
    
    /// <summary>
    /// Explode the object in question after some time
    /// </summary>
    /// <param name="time"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    IEnumerator ExplodeAfterTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        transform.position = obj.transform.position;
        GameManager.Instance.mainAudioSource.PlayOneShot(explosionSound);
        Explode();
    }
    
    /// <summary>
    /// Overload method, public
    /// Called when want to explode a certain object after a certain time.  
    /// </summary>
    /// <param name="time"></param>
    public void Explode(float time, GameObject obj)
    {
        StartCoroutine(ExplodeAfterTime(time, obj));
    }
}
