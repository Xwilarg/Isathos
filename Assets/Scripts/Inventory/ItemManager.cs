using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField]
        private SO.ItemIcons _icons;

        public static ItemManager S;

        private Dictionary<ItemID, Item> _items;

        private void Awake()
        {
            S = this;
            _items = new Dictionary<ItemID, Item>
            {
                { ItemID.HUD, new Item("HUD", "This item somehow allow you to see important information about yourself in front on you. It's probably magic or something.", _icons.UI) },
                { ItemID.HOUSE_KEY, new Item("Key", "A key. It probably opens something, but what?", _icons.Key) },
                { ItemID.SALENAE_RING, new Item("Salenae's ring", "A ring gave to you by Salenae, you don't know it's power but it seems fairly important to her.", _icons.Ring) },
            };
        }

        public Item GetItem(ItemID id)
            => _items[id];
    }
}