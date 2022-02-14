using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public Rigidbody2D hook;

    public GameObject[] prefabRopeSegs;

    public int numLinks = 3;

    void Start()
    {
        GenerateRope();
        
        //need to provide a force forward that it shoots forward into an object, however..
    }

    void GenerateRope()
    {
        Rigidbody2D prevRb = hook;
        for (int i = 0; i < numLinks; i++)
        {
            int segIndex = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[segIndex]);

            //set rope as segments' parent, as well as position
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            
            //set connectedBody as prevRb
            newSeg.GetComponent<HingeJoint2D>().connectedBody = prevRb;

            prevRb = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
