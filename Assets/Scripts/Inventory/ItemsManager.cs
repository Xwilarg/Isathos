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
        _items = new Dictionary<ItemID, Item>
        {
            { ItemID.SALENAE_RING, new Item("Salenae's ring", "A ring gave to you by Salenae, You don't know it's power but it seems fairly important to her.", _icons.Ring) },
            { ItemID.HUD, new Item("HUD", "This item somehow allow you to see important information about yourself in front on you. It's probably magic or something.", _icons.Ring) }
        };
    }

    public Item GetItem(ItemID id)
        => _items[id];
}