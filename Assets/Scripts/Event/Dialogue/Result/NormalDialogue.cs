using Other;

namespace Event.Dialogue.Result
{
    public struct NormalDialogue : IDialogueResult
    {
        public NormalDialogue(bool isSpeaking, string text, FacialExpression exp, string nameOverride = null)
        {
            IsSpeaking = isSpeaking;
            Text = text;
            NameOverride = nameOverride;
            Expression = exp;

            Speaker = Character.NONE;
        }

        public bool IsSpeaking;
        public string Text;
        public string NameOverride;
        public FacialExpression Expression;

        // THE FOLLOWING IS USED FOR MIXED DIALOGUES
        public Other.Character Speaker;

        public NormalDialogue(Character speaker, bool isSpeaking, string text, FacialExpression exp, string nameOverride = null)
        {
            IsSpeaking = isSpeaking;
            Text = text;
            NameOverride = nameOverride;
            Expression = exp;

            Speaker = speaker;
        }
    }
}