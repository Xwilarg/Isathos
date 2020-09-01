using System;
using System.Collections.Generic;

namespace Inventory
{
    public class Bag
    {
        public Bag()
        {
            _items = new Dictionary<ItemID, int>();
        }

        public void AddItem(ItemID id)
        {
            if (_items.ContainsKey(id))
                _items[id]++;
            else
                _items.Add(id, 1);
        }

        public void RemoveItem(ItemID id)
        {
            if (!_items.ContainsKey(id))
                throw new InvalidOperationException("Can't remove " + id + " from inventory");
            if (_items[id] > 1)
                _items[id]--;
            else
                _items.Remove(id);
        }

        public Dictionary<ItemID, int> GetItems()
            => _items;

        private Dictionary<ItemID, int> _items;
    }
}