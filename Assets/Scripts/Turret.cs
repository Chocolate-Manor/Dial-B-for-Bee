using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Transform rayStart;

    [SerializeField] 
    private Transform turretHead;

    [SerializeField] private Animator turretAnimator;
    private void OnTriggerStay2D(Collider2D other)
    {
        //if player enter line of sight, and more than one light is illuminating the player, kill the player. 
        if (other.CompareTag("Player"))
        {
            if (NotObstructed(other) && GameManager.instance.IsIlluminateed())
            {
                Debug.Log("You are hit!");
                turretAnimator.enabled = false;
                turretHead.up = (other.transform.position - turretHead.transform.position) * -1.0f;
                
                GameManager.instance.GameOver();
            }
        }
    }

    private bool NotObstructed(Collider2D other)
    {
        Vector3 direction = other.transform.position - rayStart.position;
        Debug.DrawRay(rayStart.position, direction, Color.red);
        //Debug.Log(Physics2D.Raycast(rayStart.position, direction).collider.name);
        return Physics2D.Raycast(rayStart.position, direction).collider.CompareTag("Player");
    }
}
