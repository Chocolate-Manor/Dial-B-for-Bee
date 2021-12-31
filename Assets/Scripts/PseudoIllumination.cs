using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoIllumination : MonoBehaviour
{
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.lightsYouAreIlluminatedBy.Add(other); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.lightsYouAreIlluminatedBy.Remove(other);
        }
    }


}