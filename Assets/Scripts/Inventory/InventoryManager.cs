using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image selectedItemImage;
        [SerializeField] private AudioClip scrollSound;

        private InventoryEntry _selectedInventoryEntry;

        private void Update()
        {
            UpdateSelectedItem();
        }

        private void UpdateSelectedItem()
        {
            // Check mouse wheel to change selected item
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                GameManager.Instance.PlaySoundEffect(scrollSound);
                _selectedInventoryEntry = inventory.GetNext();
                UpdateInventoryUI();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                GameManager.Instance.PlaySoundEffect(scrollSound);
                _selectedInventoryEntry = inventory.GetPrevious();
            }
        }

        private void UpdateInventoryUI()
        {
            countText.text = _selectedInventoryEntry.Count.ToString();
            selectedItemImage.sprite = _selectedInventoryEntry.Item.GetIcon();
        }
    }
}