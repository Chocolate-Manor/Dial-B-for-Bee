using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            // _items = new Dictionary<int, InventoryEntry>();
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
                if (inventoryEntry.Item.GetId() == itemId)
                {
                    res = inventoryEntry.Count;
                    break;
                }
            }

            return res;
        }

        public void AddItem(Item item)
        {
            if (_itemIds.Contains(item.GetId()))
            {
                this.IncreaseCountByItemId(item.GetId());
            }
            else
            {
                _itemIds.Add(item.GetId());
                _itemsList.AddLast(new InventoryEntry(item, 1));
            }
        }

        private void IncreaseCountByItemId(int id)
        {
            foreach (InventoryEntry itemEntry in _itemsList)
            {
                if (itemEntry.Item.GetId() == id)
                {
                    itemEntry.IncreaseCount();
                }
            }
        }

        private void DecreaseCountByItemId(int id)
        {
            foreach (InventoryEntry itemEntry in _itemsList)
            {
                if (itemEntry.Item.GetId() == id)
                {
                    itemEntry.IncreaseCount();
                }
            }
        }

        public void RemoveItem(Item item)
        {
            this.RemoveItem(item.GetId());
        }

        public void RemoveItem(int itemId)
        {
            if (_itemIds.Contains(itemId))
            {
                foreach (InventoryEntry inventoryEntry in _itemsList)
                {
                    if (inventoryEntry.Item.GetId() == itemId)
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

        public void Clear()
        {
            _itemIds = new HashSet<int>();
            _itemsList = new LinkedList<InventoryEntry>();
        }

        public void ClearItem(Item item)
        {
            this.ClearItem(item.GetId());
        }

        public void ClearItem(int itemId)
        {
            _itemIds.Remove(itemId);
            //TODO REMOVE FROM LINKED LIST
        }

        public List<InventoryEntry> GetAllItems()
        {
            return _itemsList.ToList();
        }

        public int GetUniqueItemCount()
        {
            return _itemsList.Count;
        }

        public InventoryEntry GetNext()
        {
            if (_finger == null)
            {
                _finger = _itemsList.First;
            }

            InventoryEntry res = _finger.Value;
            _finger = _finger.Next;
            return res;
        }

        public InventoryEntry GetPrevious()
        {
            if (_finger == null)
            {
                _finger = _itemsList.Last;
            }

            InventoryEntry res = _finger.Value;
            _finger = _finger.Previous;
            return res;
        }
    }
}