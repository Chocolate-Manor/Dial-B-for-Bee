using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSegment : MonoBehaviour
{
    //public GameObject connectedAbove, connectedBelow;
    
    /// <summary>
    /// This is for setting the connectedAnchor for every ropeSegment. 
    /// </summary>
    private void Start()
    {
        //connectedBody is the segment that's above you. 
        GameObject connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        WebSegment aboveSegment = connectedAbove.GetComponent<WebSegment>();

        if (aboveSegment != null)
        {
            //current object is the aboveSegment's below object
            //aboveSegment.connectedBelow = gameObject;
            
            //bounds size y gives the length of the rope (length of bounding box). So the new bottom will be -y. 
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y; 
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }
}
