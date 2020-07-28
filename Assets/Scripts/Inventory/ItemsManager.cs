using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    [SerializeField]
    private ItemIcons _icons;

    public static ItemsManager S;

    private Dictionary<ItemID, Item> _items;

    private void Awake()
    {
        S = this;
        _items = new Dictionary<ItemID, Item>();
        _items.Add(ItemID.SALENAE_RING, new Item("Salenae's ring", "A ring gave to you by Salenae, it seams rather powerful but also fairly important to her.", _icons.Ring));
    }

    public Item GetItem(ItemID id)
        => _items[id];
}