using System;
using System.Collections.Generic;
using System.Linq;

public class EtahniaDialogue : ADialogue
{
    public override IDialogueResult GetDialogue(int lastChoiceId)
    {
        var result = _current(lastChoiceId);
        _currProgress++;
        return result;
    }

    /// <summary>
    /// When the player first encounter the character
    /// </summary>
    private IDialogueResult Intro(int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(true, "Hey, you!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 1) return new NormalDialogue(false, "Me?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 2) return new NormalDialogue(true, "Wow it's been such a long time since I've seen someone!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 3) return new NormalDialogue(true, "How did you even managed to come here?", FacialExpression.SMILE, _knownName);
        if (_currProgress == 4) return new NormalDialogue(false, "I'm... actually a bit lost..", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 5) return new NormalDialogue(true, "Lost? No wayy! No one get lost while traveling to alternative plans!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 6) return new NormalDialogue(true, "...Well you indeed seams a bit lost. Okay! You can ask me any question you want.", FacialExpression.NEUTRAL, _knownName);

        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, lastChoiceId);
    }

    private IDialogueResult WhoAreYou(int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "Who are you?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1)
        {
            _knownName = "Etahnia";
            return new NormalDialogue(true, "Eheh, I might not look like it but I'm a Celestial and my name is Etahnia!", FacialExpression.SMILE, _knownName);
        }
        if (_currProgress == 2) return new NormalDialogue(false, "A celestial? What is it?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 3) return new NormalDialogue(true, "I guess that make sence that a human never heard of us... I think you call us \"angel\" or something?", FacialExpression.SMILE, _knownName);
        if (_currProgress == 4) return new NormalDialogue(true, "We helped your race against demons during the Great War and because of how we looks, the name sticked in.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 5) return new NormalDialogue(true, "Aa~ But that was like 6000 years ago so I don't expect you to know much about it, I wasn't even born anyway!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 6) return new NormalDialogue(false, "Aren't \"angels\" supposed to have wings or something?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 7) return new NormalDialogue(true, "Going straight where its hurt, uh?", FacialExpression.SMILE, _knownName);
        if (_currProgress == 8) return new NormalDialogue(true, "Well that's a long story, but to summarize I wanted to become a god since... well, celestian society have lot of issues but let's not speak of that right now.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 9) return new NormalDialogue(true, "Anyway, when the council heard of it they were like, really angry and they decided to banish me to an empty plan as a punishment and as I sign of this, they tearred off my wings and removed my halo.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 10) return new NormalDialogue(false, "Celestial society are pretty rough, uh?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 11) return new NormalDialogue(true, "You don't say, all behaving mightly like \"Etahnia, respect your elders\", \"Etahnia you shall not gather power\", \"Etahnia your room is such a mess\", like, come on!", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 12) return new NormalDialogue(false, "...", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 13) return new NormalDialogue(true, "But enough speaking of me, I'm sure there are plenty of others things you would like to know!", FacialExpression.SMILE, _knownName);

        _introChoice.Remove(WhoAreYou);
        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, lastChoiceId);
    }

    private IDialogueResult WhoAmI(int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "Would you know who am I?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(true, "You're asking me? You know, I met you less than one minute ago.", FacialExpression.SMILE, _knownName);
        if (_currProgress == 2) return new NormalDialogue(true, "But from your clothes, you're probably a mage or something... You probably crossed through the plans to come here, but isn't this one supposed to be sealed?", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 3) return new NormalDialogue(true, "Well I'm not sure to understand either, but if you managed to come here you must be hella strong!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 4) return new NormalDialogue(false, "I can't remember anything about that or why I came here", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 5) return new NormalDialogue(true, "Well if you don't remember there's no use digging on it! I'm sure it'll come back in due time. Anything else you would like to know?", FacialExpression.SMILE, _knownName);

        _introChoice.Remove(WhoAmI);
        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, lastChoiceId);
    }

    private IDialogueResult WhereAmI(int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "Where am I? Everything is white here.", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(true, "Well explaining everything in detail would be far too complicated but like, the world contains alternative plans, it's like you just went to another planet.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 2) return new NormalDialogue(true, "Some of them are material, like the one where you come, probably Hβ or Hγ and some are immaterials like the divine plan, Σ0.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 3) return new NormalDialogue(true, "Most of the plans follow their world rules but some of them have additional ones, that's the role of the god Yrr.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 4) return new NormalDialogue(true, "But like, anyone can travel through the plans, you just need to find a place where one of the three gates, magic, spirit or ether is weaker, but everyone that want to keep their carnal envelope just use the magic one.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 5) return new NormalDialogue(true, "And... Wait... Maybe I shouldn't tell that much to a mere human, uh?", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 6) return new NormalDialogue(true, "Well just think of this place of a material world overwritten by rules to make it subsist with nothing.", FacialExpression.SMILE, _knownName);
        if (_currProgress == 7) return new NormalDialogue(false, "(I didn't understand anything... But I'm not sure I want to hear the whole thing again.)", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 8) return new NormalDialogue(true, "Anything enough with all that theorical stuff, do you have any others questions?", FacialExpression.SMILE, _knownName);

        _introChoice.Remove(WhereAmI);
        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, lastChoiceId);
    }

    private IDialogueResult NoQuestion(int lastChoiceId)
    {
        return null;
    }

    private IDialogueResult AskQuestion(Dictionary<Func<int, IDialogueResult>, string> choices, int choiceId)
    {

        _currProgress = 0;
        _current = choices.ElementAt(choiceId).Key;
        return _current(choiceId);
    }

    private Dictionary<Func<int, IDialogueResult>, string> _introChoice;
    private Func<int, IDialogueResult> _current;
    private string _knownName;

    public EtahniaDialogue() : base()
    {
        _current = Intro;
        _knownName = "???";

        _introChoice = new Dictionary<Func<int, IDialogueResult>, string>();
        _introChoice.Add(WhoAreYou, "Who are you?");
        _introChoice.Add(WhereAmI, "Where am I?");
        _introChoice.Add(WhoAmI, "Who am I?");
        _introChoice.Add(NoQuestion, "That's okay for now");
    }
}