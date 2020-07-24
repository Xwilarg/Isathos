public class EtahniaDialogue : ADialogue
{
    public override DialogueResult? GetDialogue()
    {
        DialogueResult? result = null;
        if (_currProgress == 0) result = new DialogueResult(true, "Hey, you!", "???");
        else if (_currProgress == 1) result = new DialogueResult(false, "Me?", "Me");
        else if (_currProgress == 2) result = new DialogueResult(true, "Wow it's been so long since I've seen someone!", "???");
        else if (_currProgress == 3) result = new DialogueResult(true, "How did you even managed to come here?", "???");
        else if (_currProgress == 4) result = new DialogueResult(false, "I'm... actually a bit lost", "Me");
        else if (_currProgress == 5) result = new DialogueResult(true, "Lost? No wayy! No one get lost while traveling to alternative plans", "???");
        else if (_currProgress == 6) result = new DialogueResult(true, "...Well you indeed seams a bit lost. Okay! You can ask me any question you want!", "???");
        _currProgress++;
        return result;
    }
}