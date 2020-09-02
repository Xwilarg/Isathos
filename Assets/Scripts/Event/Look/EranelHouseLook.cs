using Inventory;
using System.Collections.Generic;

namespace Event.Look
{
    public class EranelHouseLook : ALook
    {
        public EranelHouseLook()
        {
            _books = new List<string>();
        }

        private List<string> _books;

        public override string GetText(string name)
        {
            string res = null;
            if (name == "LIBRARY1" || name == "LIBRARY2" || name == "LIBRARY3")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with books written in celestian so I can't read them.";
            }
            else if (name == "LIBRARY4")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with some books written in fairy dialect so I can't read them.";
            }
            else if (name == "LIBRARY5" || name == "LIBRARY6")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with books written in ancient elven so I can't read them.";
            }
            else if (name == "LIBRARY7")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with some books written in elven, they mostly contains stories for children.";
                if (!_books.Contains(name))
                {
                    _books.Add(name);
                    EventManager.S.DisplayNewItem(ItemID.BOOK_CHILDREN_1);
                }
            }
            else if (name == "LIBRARY8" || name == "LIBRARY9" || name == "LIBRARY10")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with books written in human language, most of them speak of human society but some of them do speak about gods.";
                if (!_books.Contains(name))
                {
                    if (name == "LIBRARY8")
                    {
                        EventManager.S.DisplayNewItem(ItemID.BOOK_HUMAN_CREATION);
                    }
                    if (name == "LIBRARY9")
                    {
                        EventManager.S.DisplayNewItem(ItemID.BOOK_HUMAN_GODS);
                    }
                    if (name == "LIBRARY10")
                    {
                        EventManager.S.DisplayNewItem(ItemID.BOOK_SPELL_SUMMON);
                    }
                    _books.Add(name);
                }
            }
            else if (name == "LIBRARY11")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with some books written in demonic dialect so I can't read them.";
            }
            else if (name == "WINDOW")
            {
                if (_currProgress == 0)
                    res = "You can only see trees outside.";
            }
            else if (name == "CUPBOARD1")
            {
                if (_currProgress == 0)
                    res = "Shelfs are filled with canned goods.";
            }
            else if (name == "CUPBOARD2")
            {
                if (_currProgress == 0)
                    res = "Shelfs are filled with canned goods.";
            }
            else if (name == "CUPBOARD3")
            {
                if (_currProgress == 0)
                {
                    if (!_books.Contains(name))
                        res = "Shelfs are filled with teas and sweets... Nobody will notice if I take only one.";
                    else
                        res = "Shelfs are filled with teas and sweets.";
                }
                if (!_books.Contains(name))
                {
                    EventManager.S.DisplayNewItem(ItemID.CANDY);
                    _books.Add(name);
                }
            }
            else if (name == "PLATES")
            {
                if (_currProgress == 0)
                    res = "Lot of dirty plates stacked on each others.";
            }
            else if (name == "LIBRARYLR")
            {
                if (_currProgress == 0)
                    res = "The bookshelf is filled with novels.";
            }
            else if (name == "CUPBOARDLR")
            {
                if (_currProgress == 0)
                    res = "The cupboard is filled with decorations.";
            }
            else if (name == "CAKE")
            {
                if (_currProgress == 0)
                    res = "A yellow cake. I have no idea what is inside but I surely don't want to try it.";
            }
            else if (name == "COMPUTER")
            {
                ComputerManager.S.StartComputer();
            }
            _currProgress++;
            return res;
        }
    }
}