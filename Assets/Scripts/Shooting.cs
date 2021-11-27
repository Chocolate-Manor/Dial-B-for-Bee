using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    
    // list of available bugs and the amount of them in the inventory
    public List<GameObject> bugs;
    public List<int> bugCounts;
    public List<Sprite> bugSprites;

    // the text object for amount of bugs
    public TextMeshProUGUI bugCountText;
    public Image selectedBugImg;

    // index of currently selected bugg
    private int _selectedBug;

    [SerializeField] private GameObject flashlight;

    public float offset = 1;

    // Start is called before the first frame update
    void Start()
    {
        flashlight.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        // update selected bugg
        InventoryControl();
       
        // update inventory UI 
        InventoryUIControl();

        // shoot selected bugg if there is inventory for it
        if (Input.GetKeyDown(KeyCode.Mouse0) && bugCounts[_selectedBug] > 0)
        {
            bugCounts[_selectedBug] -= 1;
            var bullet = Instantiate(bugs[_selectedBug], transform.position + transform.up * offset, Quaternion.identity) as GameObject;
            bullet.transform.rotation = transform.rotation;
        }

        //flashlight
        FlashlightControl();
    }

    /// <summary>
    /// Controls the flashlight. Put in update.
    /// Also set if flashlight is on in game manager. 
    /// </summary>
    private void FlashlightControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            flashlight.SetActive(true);
            GameManager.instance.flashlightOn = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            flashlight.SetActive(false);
            GameManager.instance.flashlightOn = false;
        }
    }

    /// <summary>
    /// Read scrollbar to update selected bug
    /// </summary>
    private void InventoryControl()
    {
        
        // Check mouse wheel to change selected bugg
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (_selectedBug >= bugs.Count - 1)
                _selectedBug = 0;
            else
                _selectedBug += 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
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
}