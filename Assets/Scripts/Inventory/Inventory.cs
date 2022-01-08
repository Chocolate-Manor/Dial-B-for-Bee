using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public struct InventoryEntry
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
        private Dictionary<int, InventoryEntry> _items;

        public Inventory()
        {
            _items = new Dictionary<int, InventoryEntry>();
        }


        public bool ContainsItem(int itemId)
        {
            return _items.ContainsKey(itemId);
        }

        public int GetCount(int itemId)
        {
            if (!_items.ContainsKey(itemId))
            {
                return 0;
            }

            return _items[itemId].Count;
        }

        public void AddItem(Item item)
        {
            if (_items.ContainsKey(item.GetId()))
            {
                _items[item.GetId()].IncreaseCount();
            }
            else
            {
                _items.Add(item.GetId(), new InventoryEntry(item, 1));
            }
        }

        public void RemoveItem(Item item)
        {
            this.RemoveItem(item.GetId());
        }

        public void RemoveItem(int itemId)
        {
            if (_items.ContainsKey(itemId))
            {
                _items[itemId].DecreaseCount();
                if (_items[itemId].Count == 0)
                {
                    _items.Remove(itemId);
                }
            }
        }

        public void Clear()
        {
            _items = new Dictionary<int, InventoryEntry>();
        }

        public void ClearItem(Item item)
        {
            this.ClearItem(item.GetId());
        }

        public void ClearItem(int itemId)
        {
            _items.Remove(itemId);
        }

        public List<InventoryEntry> GetAllItems()
        {
            return _items.Values.ToList();
        }
    }
}