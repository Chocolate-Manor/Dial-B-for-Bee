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
        // rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Input.mousePosition;
        var mouseGameCoords = Camera.main.ScreenToWorldPoint(mouse);

        // update player orientation
        // transform.LookAt(new Vector3(0.0f, 0.0f, 1.0f), mouseGameCoords);

        var dir = new Vector2(mouseGameCoords.x - transform.position.x, mouseGameCoords.y - transform.position.y);
        transform.up = dir;

        // rb.MoveRotation();
        // rb.rotation = mouse.x;
        // Debug.Log(rb.rotation);

        // var moveVelocity = moveInput * speed;
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = moveInput * speed;

    }
}
