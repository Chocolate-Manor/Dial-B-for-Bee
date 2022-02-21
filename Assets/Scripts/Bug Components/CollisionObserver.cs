using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObserver : MonoBehaviour
{
    public event Action<Collision2D> OnCollision;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollision(other);
    }
}
