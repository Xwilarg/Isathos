using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/DialogueSprites", fileName = "DialogueSprites")]
    public class DialogueSprites : ScriptableObject
    {
        public Sprite Empty;
        public DialogueSpritesCharacter MC;
        public DialogueSpritesCharacter Etahnia;
        public DialogueSpritesCharacter Anael;
        public DialogueSpritesCharacter Salenae;
        public DialogueSpritesCharacter Nachi;
        public DialogueSpritesCharacter Unar;
        public DialogueSpritesCharacter ExplGod;
        public DialogueSpritesCharacter Eranel;
        public DialogueSpritesCharacter Huriane;
    }
}