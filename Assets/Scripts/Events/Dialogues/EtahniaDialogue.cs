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
        if (_currProgress == 6) return new NormalDialogue(true, "...Well you indeed seems a bit lost. Okay! You can ask me any question you want.", FacialExpression.NEUTRAL, _knownName);

        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, lastChoiceId);
    }

    private IDialogueResult WhoAreYou(int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "Who are you?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1)
            return new NormalDialogue(true, "Eheh, I might not look like it but I'm a Celestial and my name is Etahnia!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 2)
        {
            _knownName = "Etahnia";
            return new NormalDialogue(false, "A celestial? What is it?", FacialExpression.NEUTRAL, "Me");
        }
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
        if (_currProgress == 4) return new NormalDialogue(true, "But like, anyone can travel through the plans, you just need to find a place where the gates flow is weaker and use the spirit one, along with the elemental one if you want to keep your carnal envelope.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 5) return new NormalDialogue(true, "And... Wait... Maybe I shouldn't tell that much to a mere human, uh?", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 6) return new NormalDialogue(true, "Well just think of this place of a material world overwritten by rules to make it subsist with nothing.", FacialExpression.SMILE, _knownName);
        if (_currProgress == 7) return new NormalDialogue(false, "(I didn't understand anything... But I'm not sure I want to hear the whole thing again.)", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 8) return new NormalDialogue(true, "Anyway enough with all that theorical stuff, do you have any others questions?", FacialExpression.SMILE, _knownName);

        _introChoice.Remove(WhereAmI);
        if (lastChoiceId == -1) return new ChoiceDialogue(_introChoice.Select(x => x.Value).ToArray());

        return AskQuestion(_introChoice, lastChoiceId);
    }

    private IDialogueResult NoQuestion(int lastChoiceId)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "I think I'm okay for now.", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(true, "So what are you planning to do now?", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 2) return new NormalDialogue(false, "I don't know, maybe going back in my world or something...", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 3) return new NormalDialogue(true, "Since you have nothing to do, why don't you help me going out of here?", FacialExpression.SMILE, _knownName);
        if (_currProgress == 4) return new NormalDialogue(false, "How could I do that?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 5) return new NormalDialogue(true, "Um... Celestian dimentional magic is way too strong so as a human, even if you managed to come here, helping me going out really is something else, like you would need nearly godly powers...", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 6) return new NormalDialogue(true, "That's it! You just have to become a god!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 7)
        {
            QuestManager.S.UpdateQuestDescription("Become a god");
            return new NormalDialogue(false, "Becoming a god? How could I even do that?", FacialExpression.NEUTRAL, "Me");
        }
        if (_currProgress == 8) return new NormalDialogue(true, "It took me 200 years to gather knowledge and understand everything, but with my help I'm sure you can do it in 2 times less!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 9) return new NormalDialogue(false, "Humans don't live that long you know.", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 10) return new NormalDialogue(true, "Ah, I forgot about that part... One quicker step to become a god would be acknowledged by another one, I don't know much about human gods but as far as I can remember, there are 4 of them.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 11) return new NormalDialogue(true, "There is Ela, the primal human deity, Nyr and Sar who took over Hβ and Sae, who is pretty much useless... Maybe we should go with this one?", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 12)
        {
            QuestManager.S.UpdateQuestDescription("Become a god\n- Be acknowledged by Sae");
            return new NormalDialogue(false, "Sounds good, but how am I even supposed to meet her?", FacialExpression.NEUTRAL, "Me");
        }
        if (_currProgress == 13) return new NormalDialogue(true, "No idea!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 14) return new NormalDialogue(true, "Maybe you should look into her past life to understand more about her, Sae is a pretty new god, only 3000 years old.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 15) return new NormalDialogue(true, "I don't really know where such text would be kept, you should probably ask in a church.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 16)
        {
            QuestManager.S.UpdateQuestDescription("Become a god\n - Find more information about Sae");
            return new NormalDialogue(false, "I'll go then, I just have to exit by that portal looking thing, right?", FacialExpression.NEUTRAL, "Me");
        }
        if (_currProgress == 17) return new NormalDialogue(true, "Yeah... But well, if you want to speak a bit more before going, I wouldn't mind. I've been pretty lonely here all this time.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 18) return new NormalDialogue(false, "Sure, I'll consider it.", FacialExpression.NEUTRAL, "Me");
        _current = RandomConversation1;
        return null;
    }

    private IDialogueResult RandomConversation1(int _)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "How did you survive all this time without food?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(true, "Celestials can consume magic energy instead of eating food, but it's tasteless so most of us would rather eat real food instead.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 2) return new NormalDialogue(true, "But man, it's been so long since I taste something good, if you ever find something good, bring it back to me!", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 3) return new NormalDialogue(false, "Yeah sure, anything you would rather eat?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 4) return new NormalDialogue(true, "I really like sweat things but because of how our world is made we mostly do artificial sugar so it's not as good.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 5) return new NormalDialogue(false, "How is your world even like?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 6) return new NormalDialogue(true, "It's mostly made of floating islands, making our wings rather useful, but because of that we use the little land we have to grow cereals.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 7) return new NormalDialogue(false, "Sounds like a lot of hardship for people who could just magic energy...", FacialExpression.NEUTRAL, "Me");
        return null;
    }

    private IDialogueResult RandomConversation2(int _)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "So there is really nothing here, only white?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(true, "Nope nothing, even if I try affecting things the world with magic it's just consumed into magic bit by bit.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 2) return new NormalDialogue(true, "But I don't think that affect the objects from the outside, I even tried to let my clothes on the ground but they didn't even disappeared, so feel free to bring me things you find during your quest!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 3) return new NormalDialogue(false, "(And what would you have done if that wasn't the case...)", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 4) return new NormalDialogue(true, "Oh but once I found a piece of bread in the middle of nowhere, I was sick for at least many days after it.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 5) return new NormalDialogue(false, "That... sound tough...", FacialExpression.NEUTRAL, "Me");
        return null;
    }

    private IDialogueResult RandomConversation3(int _)
    {
        if (_currProgress == 0) return new NormalDialogue(false, "For how much time have you been here?", FacialExpression.NEUTRAL, "Me");
        if (_currProgress == 1) return new NormalDialogue(true, "Well since there is no notion of day and night it's a bit hard to keep track of time but it's sure been a long time.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 2) return new NormalDialogue(true, "At the beginning it was really hard but then I realized that there was a high amount of magic in the air, probably used to keep the worlds of this rule alive.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 3) return new NormalDialogue(true, "So to pass time I trained myself but since I have nothing to draw magic circles I can barely move the flow of magic without doing anything with it.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 4) return new NormalDialogue(true, "But after some time I began to be more perceptive about that kind of stuff, so it was kinda motivating to see my efforts pay off.", FacialExpression.SMILE, _knownName);
        if (_currProgress == 5) return new NormalDialogue(false, "It's great that you found something to do.", FacialExpression.NEUTRAL, "Me");
        return null;
    }

    private IDialogueResult RandomConversationThanks(int _)
    {
        if (_currProgress == 1) return new NormalDialogue(true, "You know, I've been really lonely all this time, not being able to speak to anyone was really tough.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 2) return new NormalDialogue(true, "Back home, I wasn't really sociable neither I had a lot of friends, but it's hard to notice how important the small talks I had were important until I lost them.", FacialExpression.NEUTRAL, _knownName);
        if (_currProgress == 3) return new NormalDialogue(true, "So I'm really happy that you took all that time to speak with me, it really warm my heart!", FacialExpression.SMILE, _knownName);
        if (_currProgress == 4) return new NormalDialogue(true, "I really hope you will be able to find lot of cool stuffs outside, and when you find them, bring them back here!", FacialExpression.SMILE, _knownName);
        return null;
    }

    private IDialogueResult RandomConversationEnd(int _)
    {
        if (_currProgress == 1) return new NormalDialogue(true, "If you find anything during your journey, show it to me!", FacialExpression.SMILE, _knownName);
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