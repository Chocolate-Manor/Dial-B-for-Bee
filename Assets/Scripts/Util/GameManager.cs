using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        CheckInstance(); //avoid duplicates
        lightsYouAreIlluminatedBy = new List<Collider2D>();
        flashlightOn = false;
        //DontDestroyOnLoad(gameObject);

    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //-----------------------------------------//

    public List<Collider2D> lightsYouAreIlluminatedBy;
    public bool flashlightOn;
    
    public void GameOver()
    {
        Destroy(GameObject.FindWithTag("Player"));
    }

 

    public bool IsIlluminateed()
    {
        if (flashlightOn || lightsYouAreIlluminatedBy.Count > 0)
        {
            return true;
        }

        return false;
    }
}