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
                { ItemID.BOOK_CHILDREN_1, new Item("Goblins can't be elves", "A children story about a goblin that wanted to be an elf.", _icons.BookBlue) },
                { ItemID.BOOK_HUMAN_CREATION, new Item("World genesis", "A book about the creation of the world and the humans.", _icons.BookBrown) },
                { ItemID.BOOK_HUMAN_GODS, new Item("The 4 true gods", "A book about the human gods.", _icons.BookBrown) },
                { ItemID.BOOK_SPELL_SUMMON, new Item("How to invite stangers in your world", "A spell book that explains how to invite random strangers in your world.", _icons.BookOldSpell) },
            };
        }

        public Item GetItem(ItemID id)
            => _items[id];
    }
}