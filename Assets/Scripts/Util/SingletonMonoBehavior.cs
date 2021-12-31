using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour 
{
    public static T Instance { get; private set; }
    
    //we need it to be protected and virtual because:
    //protected means it is only accessible by inheriting classes (and not public),
    //and virtual means it can be overriden by inheriting classes,
    //with the override keyword. 
    
    //If you want to do inherited methods that can be overriden, protected virtual is best practice
    
    //also, in Unity, the Awake methods implemented here are automatically being called by its children if it is implemented here.
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
