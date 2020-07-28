using UnityEngine;

public class Item
{
    public Item(string name, string description, Sprite icon)
    {
        _name = name;
        _description = description;
        _icon = icon;
    }

    public void InitItemPopup(NewItem popup)
    {
        popup.Init(_name, _icon);
    }

    private string _name;
    private string _description;
    private Sprite _icon;
}