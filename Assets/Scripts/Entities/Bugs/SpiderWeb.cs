using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    [SerializeField] private Rigidbody2D firstSeg;
    void Start()
    {
        //firstSeg.velocity = transform.up * 50;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            firstSeg.velocity = transform.up * 50; 
        } 
    }
}
