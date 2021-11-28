using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDamagable
{
    [SerializeField] private Transform rayStart;
    [SerializeField] public LineRenderer m_lineRenderer;

    [SerializeField] private Transform turretHead;

    [SerializeField] private Animator turretAnimator;
    [SerializeField] private Animator laserAnimator;

    [SerializeField] private GameObject theEntireTurret;
    
    [SerializeField] private GameObject exploder;
    [SerializeField] private AudioClip laser;
    
    private void Start()
    {
        m_lineRenderer.useWorldSpace = true;
        m_lineRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        IDamagable damagable = other.GetComponentInChildren<IDamagable>();

        if (damagable != null)
        {
            //if player enter line of sight, and more than one light is illuminating the player, kill the player. 
            if (other.CompareTag("Player"))
            {
                if (NotObstructed(other) && GameManager.instance.IsIlluminateed())
                {
                    ShootRay(other);
                    damagable.Damage();
                }
            }

            //if it's other objects that carry light
            if (other.CompareTag("Detectable"))
            {
                ShootRay(other);
                damagable.Damage();
            }
        }
    }


    IEnumerator stopTurretFor(float time)
    {
        turretAnimator.enabled = false;
        yield return new WaitForSeconds(time);
        turretAnimator.enabled = true;
    }

    private void ShootRay(Collider2D other)
    {
        //turretHead.up = (other.transform.position - turretHead.transform.position) * -1.0f;
        StartCoroutine(stopTurretFor(2f));
        
        m_lineRenderer.SetPosition(0, rayStart.position);
        m_lineRenderer.SetPosition(1, other.transform.position);
        laserAnimator.SetTrigger("ShootLaser");
        GameManager.instance.PlaySoundEffect(laser);
    }

    private bool NotObstructed(Collider2D other)
    {
        Vector3 direction = other.transform.position - rayStart.position;
        //Debug.DrawRay(rayStart.position, direction, Color.red);
        //Debug.Log(Physics2D.Raycast(rayStart.position, direction).collider.name);
        return Physics2D.Raycast(rayStart.position, direction).collider.CompareTag("Player");
    }

    private bool isDamaged = false;

    public void Damage()
    {
        if (!isDamaged)
        {
            isDamaged = true;
            Instantiate(exploder).GetComponent<Explosion>().Explode(0.1f, gameObject);
            Destroy(theEntireTurret, 5f);
            theEntireTurret.SetActive(false);
        }
    }


}