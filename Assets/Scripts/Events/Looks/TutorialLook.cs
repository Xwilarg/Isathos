public class TutorialLook : ALook
{
    public override string GetText(string _)
    {
        if (_currProgress == 1)
            return null;
        IncreaseProgress();
        switch (TutorialManager.S.GetProgression())
        {
            case TutorialProgression.BEGINNING: // If the player try to go to the human world without speaking to Etahnia
                return "I can't remember what I'm doing here... maybe I should go speak to that girl first.";

            case TutorialProgression.ETAHNIA_KILL_INTRO: // If the player try to go back after being in Etahnia's world
                return "I can't just go back without doing anything.";

            default:
                return null;
        }
    }
}