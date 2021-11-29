using System;
using System.Collections;
using System.Collections.Generic;
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

    // the text object for amount of bugs
    public TextMeshProUGUI bugCountText;
    public Image selectedBugImg;

    [SerializeField] private AudioClip throwSound;

    [SerializeField] private AudioClip scrollSound;

    [SerializeField] private AudioClip errorSound;


    // index of currently selected bug
    private int _selectedBug;


    public float offset = 2;


    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            // update selected bugg
            InventoryControl();

            // update inventory UI 
            InventoryUIControl();

            // shoot selected bug if there is inventory for it
            if (Input.GetKeyDown(KeyCode.Mouse0) && bugCounts[_selectedBug] > 0)
            {
                GameManager.instance.PlaySoundEffect(throwSound);
                bugCounts[_selectedBug] -= 1;
                var bullet =
                    Instantiate(bugs[_selectedBug], transform.position + transform.up * offset,
                        Quaternion.identity) as GameObject;
                bullet.transform.rotation = transform.rotation;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && bugCounts[_selectedBug] <= 0)
            {
                GameManager.instance.PlaySoundEffect(errorSound);
            }

            //flashlight
            //FlashlightControl();
        }
    }

    // /// <summary>
    // /// Controls the flashlight. Put in update.
    // /// Also set if flashlight is on in game manager. 
    // /// </summary>
    // private void FlashlightControl()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         GameManager.instance.PlaySoundEffect(flashlightSound);
    //         flashlight.SetActive(true);
    //     }
    //
    //     if (Input.GetKeyUp(KeyCode.Space))
    //     {
    //         GameManager.instance.PlaySoundEffect(flashlightSound);
    //         flashlight.SetActive(false);
    //     }
    // }

    /// <summary>
    /// Read scrollbar to update selected bug
    /// </summary>
    private void InventoryControl()
    {
        // Check mouse wheel to change selected bugg
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            GameManager.instance.PlaySoundEffect(scrollSound);
            if (_selectedBug >= bugs.Count - 1)
                _selectedBug = 0;
            else
                _selectedBug += 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            GameManager.instance.PlaySoundEffect(scrollSound);
            if (_selectedBug <= 0)
                _selectedBug = bugs.Count - 1;
            else
                _selectedBug -= 1;
        }
    }

    private void InventoryUIControl()
    {
        bugCountText.text = bugCounts[_selectedBug].ToString();
        selectedBugImg.sprite = bugSprites[_selectedBug];
    }

    public void Damage()
    {
        GameManager.instance.ReloadAfterDelay();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        LoadBugCounts();
    }

    private void LoadBugCounts()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            if (PlayerPrefs.HasKey(bugNames[i]))
            {
                bugCounts[i] = PlayerPrefs.GetInt(bugNames[i]);
            }
            else
            {
                PlayerPrefs.SetInt(bugNames[i], 0);
                bugCounts[i] = 0;
            }
        }
    }

    public void SaveBugCounts()
    {
        for (int i = 0; i < bugs.Count; i++)
        {
            PlayerPrefs.SetInt(bugNames[i], bugCounts[i]);
        }
    }
}