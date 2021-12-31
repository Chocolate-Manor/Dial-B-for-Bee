using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
   // [SerializeField] private GameObject gameManager;
   [SerializeField] private GameObject[] toBeLoaded;
   private void Awake()
   {
      foreach (var gameObject in toBeLoaded)
      {
         Instantiate(gameObject);
      } 
      
      // if (GameManager.Instance == null)
      // {
      //    Instantiate(gameManager);
      // }
   }
}
