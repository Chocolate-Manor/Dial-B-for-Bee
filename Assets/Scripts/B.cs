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
    
    [SerializeField] private AudioClip errorSound;


    // index of currently selected bug
    private int _selectedBug;
    private int indexOfLadybug;

    public float offset = 2;

    
    
    private void Start()
    {
        LoadBugCounts();
        indexOfLadybug = bugNames.FindIndex(x => x.Equals("Ladybug"));
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.IsPaused)
        {
            // shoot selected bug if there is inventory for it
            if (Input.GetKeyDown(KeyCode.Mouse0) && bugCounts[_selectedBug] > 0)
            {
                GameManager.Instance.PlaySoundEffect(throwSound);
                bugCounts[_selectedBug] -= 1;
                var bullet =
                    Instantiate(bugs[_selectedBug], transform.position + transform.up * offset,
                        Quaternion.identity) as GameObject;
                bullet.transform.rotation = transform.rotation;
                if (_selectedBug == indexOfLadybug)
                {
                    Ladybug ladybug = bullet.GetComponent<Ladybug>();
                    ladybug.isPickable = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && bugCounts[_selectedBug] <= 0)
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