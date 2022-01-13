using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : Bug, IDamagable
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.bugName = "Firefly";
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    public void Damage()
    {
        Destroy(gameObject); 
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            B b = other.gameObject.GetComponent<B>();
            PickMeUp(b);
            Destroy(gameObject);
        }
    }
}
