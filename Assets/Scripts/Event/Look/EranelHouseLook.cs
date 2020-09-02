using Inventory;
using System.Collections.Generic;

namespace Event.Look
{
    public class EranelHouseLook : ALook
    {
        public EranelHouseLook()
        {
            _books = new List<int>();
        }

        private List<int> _books;

        public override string GetText(string name)
        {
            string res = null;
            if (name == "LIBRARY1" || name == "LIBRARY2" || name == "LIBRARY3")
            {
                if (_currProgress == 0)
                    res = "The library is filled with books written in celestian so I can't read them.";
            }
            if (name == "LIBRARY4")
            {
                if (_currProgress == 0)
                    res = "The library is filled with some books written in fairy dialect so I can't read them.";
            }
            if (name == "LIBRARY5" || name == "LIBRARY6")
            {
                if (_currProgress == 0)
                    res = "The library is filled with books written in ancient elven so I can't read them.";
            }
            if (name == "LIBRARY7")
            {
                if (_currProgress == 0)
                    res = "The library is filled with some books written in elven, they mostly contains stories for children.";
                if (!_books.Contains(7))
                {
                    _books.Add(7);
                    EventManager.S.DisplayNewItem(ItemID.BOOK_CHILDREN_1);
                }
            }
            if (name == "LIBRARY8" || name == "LIBRARY9" || name == "LIBRARY10")
            {
                if (_currProgress == 0)
                    res = "The library is filled with books written in human language, most of them speak of human society but some of them do speak about gods.";
                if (name == "LIBRARY8" && !_books.Contains(8))
                {
                    _books.Add(8);
                    EventManager.S.DisplayNewItem(ItemID.BOOK_HUMAN_CREATION);
                }
                if (name == "LIBRARY9" && !_books.Contains(9))
                {
                    _books.Add(9);
                    EventManager.S.DisplayNewItem(ItemID.BOOK_HUMAN_GODS);
                }
                if (name == "LIBRARY10" && !_books.Contains(10))
                {
                    _books.Add(10);
                    EventManager.S.DisplayNewItem(ItemID.BOOK_SPELL_SUMMON);
                }
            }
            if (name == "LIBRARY11")
            {
                if (_currProgress == 0)
                    res = "The library is filled with some books written in demonic dialect so I can't read them.";
            }
            _currProgress++;
            return res;
        }
    }
}