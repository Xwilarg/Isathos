using System.Collections.Generic;

public class Inventory
{
    public Inventory()
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

    public Dictionary<ItemID, int> GetItems()
        => _items;

    private Dictionary<ItemID, int> _items;
}