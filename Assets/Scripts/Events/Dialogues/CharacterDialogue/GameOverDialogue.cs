using System;
using UnityEngine.SceneManagement;

public class GameOverDialogue : ADialogue
{
    public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
    {
        throw new NotImplementedException();
    }

    public IDialogueResult Defeat(EventDiscussion e, int lastChoiceId)
    {
        IDialogueResult res = null;
        if (_currProgress == 0) res = new NormalDialogue(true, "My, I'm not sure what you were expecting with this one.", FacialExpression.SMILE, _knownName);
        else if (_currProgress == 1) res = new NormalDialogue(true, "I usually would try to give you a piece of advice but I don't have much to say when you are reckless like that.", FacialExpression.NEUTRAL, _knownName);
        else if (_currProgress == 2) res = new NormalDialogue(true, "Try to not threaten people when you have no mean of fighting back, I guess.", FacialExpression.SMILE, _knownName);
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        IncreaseProgress();
        return res;
    }
}