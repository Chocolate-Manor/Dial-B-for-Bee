using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryEntry
    {
        public Item Item;
        public int Count;

        public InventoryEntry(Item item, int count)
        {
            this.Item = item;
            this.Count = count;
        }

        public void IncreaseCount()
        {
            this.Count++;
        }

        public void DecreaseCount()
        {
            if (this.Count > 0)
            {
                this.Count--;
            }
        }
    }

    public class Inventory : MonoBehaviour
    {
        private HashSet<int> _itemIds;
        private LinkedList<InventoryEntry> _itemsList;
        private LinkedListNode<InventoryEntry> _finger;

        public Inventory()
        {
            _itemsList = new LinkedList<InventoryEntry>();
            _itemIds = new HashSet<int>();
        }


        public bool ContainsItem(int itemId)
        {
            return _itemIds.Contains(itemId);
        }

        public int GetCount(int itemId)
        {
            if (!_itemIds.Contains(itemId))
            {
                return 0;
            }

            int res = 0;
            foreach (InventoryEntry inventoryEntry in _itemsList)
            {
                if (inventoryEntry.Item.id == itemId)
                {
                    res = inventoryEntry.Count;
                    break;
                }
            }

            return res;
        }

        public void AddItem(Item item)
        {
            if (_itemIds.Contains(item.id))
            {
                this.IncreaseCountByItemId(item.id);
            }
            else
            {
                _itemIds.Add(item.id);
                _itemsList.AddLast(new InventoryEntry(item, 1));
            }
        }

        private void IncreaseCountByItemId(int id)
        {
            foreach (InventoryEntry itemEntry in _itemsList)
            {
                if (itemEntry.Item.id == id)
                {
                    itemEntry.IncreaseCount();
                }
            }
        }

        private void DecreaseCountByItemId(int id)
        {
            foreach (InventoryEntry itemEntry in _itemsList)
            {
                if (itemEntry.Item.id == id)
                {
                    itemEntry.DecreaseCount();
                    if (itemEntry.Count == 0)
                    {
                        _itemIds.Remove(itemEntry.Item.id);
                        _itemsList.Remove(itemEntry);
                        break;
                    }
                }
            }
        }

        public void DecreaseItemAmount(Item item)
        {
            DecreaseCountByItemId(item.id);
        }

        public void RemoveItem(Item item)
        {
            this.RemoveItem(item.id);
        }

        public void RemoveItem(int itemId)
        {
            if (_itemIds.Contains(itemId))
            {
                foreach (InventoryEntry inventoryEntry in _itemsList)
                {
                    if (inventoryEntry.Item.id == itemId)
                    {
                        inventoryEntry.DecreaseCount();
                        if (inventoryEntry.Count <= 0)
                        {
                            _itemsList.Remove(inventoryEntry);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all items from the inventory.
        /// </summary>
        public void Clear()
        {
            _itemIds = new HashSet<int>();
            _itemsList = new LinkedList<InventoryEntry>();
        }

        public void ClearItem(Item item)
        {
            this.ClearItem(item.id);
        }

        public void ClearItem(int itemId)
        {
            _itemIds.Remove(itemId);
            RemoveItem(itemId);
        }
        
        /// <summary>
        /// Returns the number of unique items in
        /// the inventory.
        /// </summary>
        /// <returns>The number of unique items in the inventory</returns>
        public int GetUniqueItemCount()
        {
            return _itemsList.Count;
        }

        /// <summary>
        /// Returns the next entry (in order of additions) in the inventory.
        /// If the current entry is the last one,
        /// it wraps around.
        /// If the inventory is empty it returns
        /// null.
        /// </summary>
        /// <returns>The next inventory entry</returns>
        public InventoryEntry GetNext()
        {
            if (_finger == null)
            {
                _finger = _itemsList.First;
            }

            InventoryEntry res = null;
            if (_finger != null)
            {
                res = _finger.Value;
                _finger = _finger.Next;
            }

            return res;
        }
        
        /// <summary>
        /// Returns the previous entry (in order of additions) in the inventory.
        /// If the current entry is the first one,
        /// it wraps around.
        /// If the inventory is empty it returns
        /// null.
        /// </summary>
        /// <returns>The next inventory entry</returns>
        public InventoryEntry GetPrevious()
        {
            if (_finger == null)
            {
                _finger = _itemsList.Last;
            }
            
            InventoryEntry res = null;
            if (_finger != null)
            {
                res = _finger.Value;
                _finger = _finger.Previous;
            }
            return res;
        }
    }
}