using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{

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
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject bullet = Instantiate(firefly, transform.position + transform.up*offset, Quaternion.identity) as GameObject;
            bullet.transform.rotation = transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject bullet = Instantiate(ladybug, transform.position + transform.up*offset, Quaternion.identity) as GameObject;
            bullet.transform.rotation = transform.rotation;
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
