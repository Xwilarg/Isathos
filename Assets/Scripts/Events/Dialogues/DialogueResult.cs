public struct DialogueResult
{
    public DialogueResult(bool isSpeaking, string text, FacialExpression exp, string nameOverride = null)
    {
        IsSpeaking = isSpeaking;
        Text = text;
        NameOverride = nameOverride;
        Expression = exp;
    }

    public bool IsSpeaking;
    public string Text;
    public string NameOverride;
    public FacialExpression Expression;
}