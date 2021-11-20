using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float smoothSpeed = 0.125f;
        
    private Vector3 _velocity = Vector3.zero;
    
    private Vector3 offset =  new Vector3(0.0f, 0.0f, -10.0f);

    // Update is called once per frame
    void FixedUpdate()
    {

        var desiredPos = target.transform.position + offset;
        var smoothedPos = Vector3.SmoothDamp(transform.position,desiredPos, ref _velocity, smoothSpeed);
        
        this.transform.position = smoothedPos;
    }
    
     
}
