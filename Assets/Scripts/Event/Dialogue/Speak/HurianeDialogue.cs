using Event.Dialogue.Result;
using Event.Trigger.EventType;
using Inventory;
using Other;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Event.Dialogue.Speak
{
    public class HurianeDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult Intro(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "Wah a stranger in our house! Nice to meet you, I'm Huriane!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 1)
            {
                _knownName = "Huriane";
                return new NormalDialogue(true, "I guess you are here from my husband? How can I help you?", FacialExpression.SMILE, _knownName);
            }
            if (_currProgress == 2) return new NormalDialogue(false, "A friend of your husband was sealed away and we are trying to free her.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(false, "To sum up I need to find information about the god Sae.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 4) return new NormalDialogue(true, "Sounds tough but I know nothing about gods so I can't help!", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 5) return new NormalDialogue(true, "However I can help you if you want to open a portal to somewhere, just bring me the motif and I will keep it open for you.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 6) return new NormalDialogue(false, "Thanks.", FacialExpression.NEUTRAL, _knownName);

            _current = Main;
            return null;
        }

        private IDialogueResult Main(EventDiscussion e, int lastChoiceId)
        {
            if (!SummonManager.S.CanSummon()) // If someone was summoned and we didn't speak with her yet
            {
                if (_currProgress == 0) return new NormalDialogue(true, "You should speak with the newcomer first.", FacialExpression.NEUTRAL, _knownName);
                return null;
            }
            if (_currProgress == 0) return new NormalDialogue(true, "How can I help?", FacialExpression.SMILE, _knownName);

            if (lastChoiceId == -1) return new ChoiceDialogue(_dialogueChoice.Select(x => x.Value).ToArray());

            return AskQuestion(_dialogueChoice, e, lastChoiceId);
        }
        private IDialogueResult StartRandomConversation(EventDiscussion e, int lastChoiceId)
        {
            if (_randomConversations.Count == 0)
                _current = DefaultConversation;
            else
            {
                _current = _randomConversations[UnityEngine.Random.Range(0, _randomConversations.Count)];
                _randomConversations.Remove(_current);
            }

            return _current(e, lastChoiceId);
        }

        private IDialogueResult RandomConversation1(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "Aren't you a bit too young to be with someone like Eranel?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(true, "Comparing ages are a bit weird since Anael have a way bigger life expectancy than me, you know.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 2) return new NormalDialogue(false, "What do you mean?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(true, "Well Anael is a celestian and he is already around 1100 years old but his race live like 1500 years so it's okay.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 4) return new NormalDialogue(true, "As for me, I'm a fairy so I can only expect to live around 40 years, and I'm only 8!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 5) return new NormalDialogue(false, "...Isn't it a bit too young to already be married?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 6) return new NormalDialogue(true, "Why that? I'm only have 30 years in front of me so I would rather be with the one I love for as much as I can!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 7) return new NormalDialogue(true, "Fairies mature really quickly compared to the others races so it's normal for us to already in couple at that age.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 8) return new NormalDialogue(true, "But most of us are rather polygamist so I guess I'm pretty much an exception on that point!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 9) return new NormalDialogue(false, "(I guess I'll still need a bit of time to be used to it...)", FacialExpression.NEUTRAL, _knownName);

            _current = Main;
            return null;
        }

        private IDialogueResult RandomConversation2(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "So both you and Arael learnt to speak human language?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(true, "It wasn't that hard for Arael, he told me that celestians learn human language as part as their curiculum.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "But for me, it was soo hard. But since Arael can't speak fairy language I had to work really hard for him!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 3) return new NormalDialogue(true, "Us, fairies, don't usually learn any other than fairy dialect and maybe elven language, so finding resources to learn the human one was really tough.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 4) return new NormalDialogue(false, "Why didn't you learn celestian instead?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 5) return new NormalDialogue(true, "Celestian is super hard to learn, like using these weird symbols everywhere and since Arael already speak human language I just went with that.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 6) return new NormalDialogue(false, "(I'm pretty sure that fairy dialect also use lot of \"weird symbols\"...)", FacialExpression.NEUTRAL, _knownName);

            _current = Main;
            return null;
        }

        private IDialogueResult DefaultConversation(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "If I can do something to help, feel free to ask me!", FacialExpression.SMILE, _knownName);

            _current = Main;
            return null;
        }

        private IDialogueResult CreatePortal(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(true, "Where do you want to go?", FacialExpression.SMILE, _knownName);
            if (lastChoiceId == -1) return new ChoiceDialogue(_unlockedGates.Select(x => x.Value).ToArray());

            return AskQuestion(_unlockedGates, e, lastChoiceId);
        }

        private IDialogueResult CreatePortalEtahnia(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0)
            {
                _unlockedGates[CreatePortalEtahnia] = "Etahnia's world"; // TODO: Probably should move that once we go in the gate
                GateManager.S.EnableGateEtahnia();
                return new NormalDialogue(true, "Here it is!", FacialExpression.SMILE, _knownName); // TODO: Weird behavior if you just leave without pressing E
            }

            _current = Main;
            return null;
        }

        private IDialogueResult CreatePortalCancel(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "Nevermind.", FacialExpression.NEUTRAL, _knownName);

            _current = Main;
            return null;
        }

        private IDialogueResult GiveItem(EventDiscussion e, int lastChoiceId)
        {
            PlayerController.S.SetIsCinematic(true);
            InventoryPopup.S.ForceOpenInventory(GetItem);
            _currProgress = 0;
            return null;
        }

        private IDialogueResult GiveDefault(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
                return new NormalDialogue(true, "There is nothing I can do with that, if you want me to open a portal you must give me a template of a magic circle.", FacialExpression.SMILE, _knownName);

            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private IDialogueResult GiveEtahnia(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
            {
                EventManager.S.RemoveItem(e, ItemID.FOLDED_PAPER);
                _unlockedGates.Add(CreatePortalEtahnia, "???");
                return new NormalDialogue(true, "This one is quite complicated, must be going to a quite far away place!", FacialExpression.SMILE, _knownName);
            }
            if (_currProgress == 1) return new NormalDialogue(true, "But it should be okay, if you want me to open it just speak to me again!", FacialExpression.SMILE, _knownName);

            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private IDialogueResult GiveRandomSummon(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
            {
                EventManager.S.RemoveItem(e, ItemID.BOOK_SPELL_SUMMON);
                return new NormalDialogue(true, "Oh this one seems interesting!", FacialExpression.SMILE, _knownName);
            }
            if (_currProgress == 1) return new NormalDialogue(true, "I guess it can be risky but I guess I'm just curious of who will come out.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "If you want to try it, speak to me again but you'll have to deal with who comes out since there is no way to send them back.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 3) return new NormalDialogue(true, "But that's not a problem, we have a second house that work as a guest house but since nobody come anymore it's rather empty.", FacialExpression.SMILE, _knownName);

            _dialogueChoice.Add(SummonStranger, "Summon stranger");
            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private IDialogueResult GiveCandy(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
            {
                EventManager.S.RemoveItem(e, ItemID.CANDY);
                return new NormalDialogue(true, "Yay candy! Thanks!", FacialExpression.SMILE, _knownName);
            }

            IncreaseRelation(e);
            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private IDialogueResult GiveNothing(EventDiscussion e, int _)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "Nevermind", FacialExpression.NEUTRAL, _knownName);

            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private IDialogueResult SummonStranger(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
            {
                PlayerController.S.SetIsCinematic(true);
                if (!SummonManager.S.Summon()) return new NormalDialogue(true, "...Looks like nobody is coming.", FacialExpression.NEUTRAL, _knownName);
                return new NormalDialogue(true, "It worked! You should go speak with her now.", FacialExpression.SMILE, _knownName);
            }
            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private void GetItem(ItemID id)
        {
            InventoryPopup.S.ForceCloseInventory();
            switch (id)
            {
                case (ItemID)(-1):
                    _current = GiveNothing;
                    break;

                case ItemID.FOLDED_PAPER:
                    _current = GiveEtahnia;
                    break;

                case ItemID.CANDY:
                    _current = GiveCandy;
                    break;

                case ItemID.BOOK_SPELL_SUMMON:
                    _current = GiveRandomSummon;
                    break;

                default:
                    _current = GiveDefault;
                    break;
            }
            EventManager.S.DisplayDiscution((EventDiscussion)PlayerController.S.GetEventTrigger().Event, -1, Character.HURIANE); // Force the display of next Etahnia dialogue
        }

        private Dictionary<Func<EventDiscussion, int, IDialogueResult>, string> _dialogueChoice;
        private List<Func<EventDiscussion, int, IDialogueResult>> _randomConversations;
        private Dictionary<Func<EventDiscussion, int, IDialogueResult>, string> _unlockedGates;
        public HurianeDialogue() : base()
        {
            _current = Intro;

            _dialogueChoice = new Dictionary<Func<EventDiscussion, int, IDialogueResult>, string>
            {
                { CreatePortal, "Create portal" },
                { GiveItem, "Discover spell" },
                { StartRandomConversation, "Speak" }
            };

            _randomConversations = new List<Func<EventDiscussion, int, IDialogueResult>>
            {
                RandomConversation1, RandomConversation2
            };

            _unlockedGates = new Dictionary<Func<EventDiscussion, int, IDialogueResult>, string>
            {
                { CreatePortalCancel, "Cancel" }
            };
        }
    }
}