using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("lol");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseGameCoords = Camera.main.ScreenToWorldPoint(mouse);

        // update player orientation
        transform.LookAt(new Vector3(0.0f, 0.0f, 1.0f), mouseGameCoords);

        if (Input.GetKeyDown(KeyCode.W)) {
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
    }
}
