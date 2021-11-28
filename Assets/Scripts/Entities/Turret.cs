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

    [SerializeField] private GameObject theEntireTurret;
    private void Start()
    {
        m_lineRenderer.useWorldSpace = true;
        m_lineRenderer.enabled = false;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        //if player enter line of sight, and more than one light is illuminating the player, kill the player. 
        if (other.CompareTag("Player"))
        {
            if (NotObstructed(other) && GameManager.instance.IsIlluminateed())
            {
                m_lineRenderer.enabled = true;
                m_lineRenderer.SetPosition(0, rayStart.position);
                m_lineRenderer.SetPosition(1, other.transform.position);
                Debug.Log("You are hit!");
                turretAnimator.enabled = false;
                turretHead.up = (other.transform.position - turretHead.transform.position) * -1.0f;
            }
            else
            {
                m_lineRenderer.enabled = false;
            }
        }
    }

    private bool NotObstructed(Collider2D other)
    {
        Vector3 direction = other.transform.position - rayStart.position;
        //Debug.DrawRay(rayStart.position, direction, Color.red);
        //Debug.Log(Physics2D.Raycast(rayStart.position, direction).collider.name);
        return Physics2D.Raycast(rayStart.position, direction).collider.CompareTag("Player");
    }

    public void Damage()
    {
        Destroy(theEntireTurret);
    }
}