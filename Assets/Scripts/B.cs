using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class B : SingletonMonoBehavior<B>, IDamagable
{
    [SerializeField] private BInventoryManager inventoryManager;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private float distanceRayOffset = 0.485f;

    
    // index of currently selected bug
    private int _selectedBug;
    private int indexOfLadybug;

    public float offset = 2;




    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            // shoot selected bug if there is inventory for it
            float dist = DistanceToColliders(0.5f);
            if (Input.GetKeyDown(KeyCode.Mouse0) && inventoryManager.HasItem() && dist == 0)
            {
                Item currentItem = inventoryManager.GetCurrentlySelectedItem();
                GameManager.Instance.PlaySoundEffect(throwSound);
                GameObject bullet = Instantiate(currentItem.associatedPrefab, transform.position + transform.up * offset,
                        Quaternion.identity);
                bullet.transform.rotation = transform.rotation;
                //triggers UI fade in animation
                GameManager.Instance.CallEventOnInventoryUpdated();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && (!inventoryManager.HasItem() || dist != 0))
            {
                GameManager.Instance.PlaySoundEffect(errorSound);
            }
        }
    }
    

    private float DistanceToColliders(float maxDist)
    {
        // ray starting at player (with offset) and in direction its facing
        Ray ray = new Ray(transform.position + distanceRayOffset * transform.up, transform.up);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDist);
       
        //Debug drawing
        //Color rayColor = hit.distance == 0 ? Color.white : Color.red;
        //Debug.DrawRay(ray.origin, ray.direction * maxDist, rayColor);
         
        return hit.distance;           
    }

    public void Damage()
    {
        GameManager.Instance.ReloadAfterDelay();
        gameObject.SetActive(false);
    }
}