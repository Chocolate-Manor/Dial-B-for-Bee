using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
   [SerializeField] private GameObject gameManager;

   private void Awake()
   {
      if (GameManager.instance == null)
      {
         Instantiate(gameManager);
      }
   }
}
