using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyController : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
    }
}
