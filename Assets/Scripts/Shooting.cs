using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{

    [SerializeField]
    private int fireFlyCount = 5;
    [SerializeField]
    private int ladyBugCount = 5;
    
    public GameObject firefly;
    public GameObject ladybug;
     
    [SerializeField]
    private GameObject flashlight;
    
    public float offset = 1;
    // Start is called before the first frame update
    void Start()
    {
        flashlight.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireFlyCount > 0)
        {
            fireFlyCount--;
            GameObject bullet = Instantiate(firefly, transform.position + transform.up*offset, Quaternion.identity) as GameObject;
            bullet.transform.rotation = transform.rotation;
        } else if (fireFlyCount <= 0)
        {
            Debug.Log("You are out of fireflies");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && ladyBugCount > 0)
        {
            ladyBugCount--;
            GameObject bullet = Instantiate(ladybug, transform.position + transform.up*offset, Quaternion.identity) as GameObject;
            bullet.transform.rotation = transform.rotation;
        } else if (ladyBugCount <= 0)
        {
            Debug.Log("You are out of ladybugs");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            flashlight.SetActive(false);
        }
    }
}