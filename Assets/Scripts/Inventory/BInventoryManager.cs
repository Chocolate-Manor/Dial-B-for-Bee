using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class BInventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Image selectedItemImage;
        [SerializeField] private AudioClip scrollSound;

        private InventoryEntry selectedInventoryEntry;

        private void Start()
        {
            countText.enabled = false;
            selectedItemImage.enabled = false;
        }

        private void Update()
        {
            UpdateSelectedItem();
        }

        /// <summary>
        /// The main method responsible for changing what item is selected.
        /// </summary>
        private void UpdateSelectedItem()
        {
            if (selectedInventoryEntry == null && inventory.GetUniqueItemCount() > 0)
            {
                selectedInventoryEntry = inventory.GetNext();
            }

            // Check mouse wheel to change selected item
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                GameManager.Instance.PlaySoundEffect(scrollSound);
                selectedInventoryEntry = inventory.GetNext();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                GameManager.Instance.PlaySoundEffect(scrollSound);
                selectedInventoryEntry = inventory.GetPrevious();
            }

            UpdateInventoryUI();
        }

        /// <summary>
        /// Updates the inventory UI.
        /// </summary>
        private void UpdateInventoryUI()
        {
            //if no item selected, turn of the 
            //UI elements.
            if (selectedInventoryEntry == null)
            {
                countText.enabled = false;
                selectedItemImage.enabled = false;
            }

            //Re-enable the UI elements if there is a selected item.
            if (!countText.enabled && selectedInventoryEntry != null)
            {
                countText.enabled = true;
            }

            if (!selectedItemImage.enabled && selectedInventoryEntry != null)
            {
                selectedItemImage.enabled = true;
            }

            if (selectedInventoryEntry != null)
            {
                int count = inventory.GetCount(selectedInventoryEntry.Item.id);
                countText.text = count.ToString();
                selectedItemImage.sprite = selectedInventoryEntry.Item.icon;
            }
        }

        /// <summary>
        /// Returns the currently selected item.
        /// </summary>
        /// <returns>The item currently selected.</returns>
        public Item GetCurrentlySelectedItem()
        {
            if (selectedInventoryEntry == null)
            {
                return null;
            }

            inventory.DecreaseItemAmount(selectedInventoryEntry.Item);
            Item res = selectedInventoryEntry.Item;
            if (inventory.GetCount(res.id) == 0)
            {
                selectedInventoryEntry = inventory.GetNext();
            }

            return res;
        }

        /// <summary>
        /// Checks if there is an item selected.
        /// </summary>
        /// <returns>True iff there is an item selected.</returns>
        public bool HasItem()
        {
            return selectedInventoryEntry != null;
        }
    }
}