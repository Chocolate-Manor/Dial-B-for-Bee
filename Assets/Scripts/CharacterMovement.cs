using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float speed = 10;
  
    [SerializeField]
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse coords
        var mouse = Input.mousePosition;
        var mouseGameCoords = Camera.main.ScreenToWorldPoint(mouse);

        // orient the transform towards mouse
        var dir = new Vector2(mouseGameCoords.x - transform.position.x, mouseGameCoords.y - transform.position.y);
        transform.up = dir;
    }

    private void FixedUpdate()
    {
        // var moveVelocity = moveInput * speed;
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = moveInput * speed;
    }
}
