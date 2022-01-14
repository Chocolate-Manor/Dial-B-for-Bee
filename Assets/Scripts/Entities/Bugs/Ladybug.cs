using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : Bug
{
    [SerializeField] private float lifeSpan = 500;
    // Destroy(gameObject, lifeSpan);

    private float initializationTime;

    private Explosion explosion;
    [SerializeField] private GameObject exploder;

    [SerializeField] private new GameObject light;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        this.bugName = "Ladybug";
        initializationTime = Time.timeSinceLevelLoad;
        rb.AddForce(transform.up * speed);

        StartCoroutine(LightTicking());

        //explode it after lifespan. 
        if (!isPickable)
        {
            Instantiate(exploder).GetComponent<Explosion>().Explode(lifeSpan, gameObject);
            Destroy(gameObject, lifeSpan + 0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isPickable)
        {
            this.PickMeUp(other.gameObject);
        }
    }

    IEnumerator LightTicking()
    {
        while (true)
        {
            light.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            light.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }
}