using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class B : MonoBehaviour, IDamagable
{
    // list of available bugs and the amount of them in the inventory
    public List<GameObject> bugs;
    public List<int> bugCounts;
    public List<Sprite> bugSprites;
    public List<String> bugNames;

    [SerializeField] private BInventoryManager inventoryManager;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip errorSound;


    // index of currently selected bug
    private int _selectedBug;
    private int indexOfLadybug;

    public float offset = 2;

    
    
    private void Start()
    {
        // LoadBugCounts();
        // indexOfLadybug = bugNames.FindIndex(x => x.Equals("Ladybug"));
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            // shoot selected bug if there is inventory for it
            if (Input.GetKeyDown(KeyCode.Mouse0) && inventoryManager.HasItem())
            {
                Item currentItem = inventoryManager.GetCurrentlySelectedItem();
                GameManager.Instance.PlaySoundEffect(throwSound);
                GameObject bullet = Instantiate(currentItem.associatedPrefab, transform.position + transform.up * offset,
                        Quaternion.identity);
                bullet.transform.rotation = transform.rotation;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !inventoryManager.HasItem())
            {
                GameManager.Instance.PlaySoundEffect(errorSound);
            }
        }
    }
    

    public void Damage()
    {
        GameManager.Instance.ReloadAfterDelay();
        gameObject.SetActive(false);
    }
}