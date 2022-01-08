using System;
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
    
    [SerializeField] private float distanceRayOffset = 0.485f;

    // index of currently selected bug
    private int selectedBug;
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
            // update selected bugg
            InventoryControl();

            // update inventory UI 
            InventoryUIControl();
            
            // shoot selected bugg if there is inventory for it (if dist=0 then no collision)
            float dist = DistanceToColliders(0.5f);
            if (Input.GetKeyDown(KeyCode.Mouse0) && bugCounts[selectedBug] > 0 && dist == 0)
            {
                GameManager.Instance.PlaySoundEffect(throwSound);
                bugCounts[selectedBug] -= 1;
                var bullet = Instantiate(bugs[selectedBug], transform.position + transform.up * offset,
                    Quaternion.identity);
                bullet.transform.rotation = transform.rotation;
                if (selectedBug == indexOfLadybug)
                {
                    Ladybug ladybug = bullet.GetComponent<Ladybug>();
                    ladybug.isPickable = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameManager.Instance.PlaySoundEffect(errorSound);
            }

            //flashlight
            //FlashlightControl();
        }
    }

    private float DistanceToColliders(float maxDist)
    {
        // ray starting at player (with offset) and in direction its facing
        Ray ray = new Ray(transform.position + distanceRayOffset * transform.up, transform.up);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDist);
       
        Color rayColor = hit.distance == 0 ? Color.white : Color.red;
        Debug.DrawRay(ray.origin, ray.direction * maxDist, rayColor);
        Debug.Log(hit.distance);
         
        return hit.distance;           
    }

    /// <summary>
    /// Read scrollbar to update selected bug
    /// </summary>
    private void InventoryControl()
    {
        // Check mouse wheel to change selected bugg
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            GameManager.Instance.PlaySoundEffect(scrollSound);
            if (selectedBug >= bugs.Count - 1)
                selectedBug = 0;
            else
                selectedBug += 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            GameManager.Instance.PlaySoundEffect(scrollSound);
            if (selectedBug <= 0)
                selectedBug = bugs.Count - 1;
            else
                selectedBug -= 1;
        }
    }

    private void InventoryUIControl()
    {
        bugCountText.text = bugCounts[selectedBug].ToString();
        selectedBugImg.sprite = bugSprites[selectedBug];
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