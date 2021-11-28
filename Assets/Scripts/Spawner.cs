using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private void Awake()
    {
        foreach (var obj in objects)
        {
            obj.transform.position = transform.position;
        }
    }
}
