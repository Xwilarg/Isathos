using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/ItemIcons", fileName = "ItemIcons")]
    public class ItemIcons : ScriptableObject
    {
        public Sprite Ring;
        public Sprite UI;
        public Sprite Key;
        public Sprite BookBlue, BookBrown, BookSpell, BookOldSpell;
        public Sprite Candy;
        public Sprite Paper;
        public Sprite Gem;
    }
}