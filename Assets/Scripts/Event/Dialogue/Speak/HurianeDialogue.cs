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
            if (_currProgress == 1) return new NormalDialogue(true, "Eheh, I may not looks like it but I'm actually around 90 years old but Eranel is only around 80!", FacialExpression.SMILE, _knownName);
            if (_currProgress == 2) return new NormalDialogue(false, "That surprisinly alike... How much is it in human age?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(true, "Human age...? How much time does human live already?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 4) return new NormalDialogue(false, "Around 80 years.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 5) return new NormalDialogue(true, "80? So celestian lifetime is around 150 years so Eranel must be around 50 or 60?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 6) return new NormalDialogue(true, "About me... fairy lifetime like around 500 so I guess I'm like... 15? Mental math aren't really my thing...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 7) return new NormalDialogue(false, "...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 8) return new NormalDialogue(true, "But it's kinda weird to think of things that way since all races don't mature at the same speed, you know.", FacialExpression.SMILE, _knownName);
            if (_currProgress == 9) return new NormalDialogue(false, "(I guess... but that still sounds weird for me)", FacialExpression.NEUTRAL, _knownName);

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
                return new NormalDialogue(true, "Here it is!", FacialExpression.SMILE, _knownName);
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
                EventManager.S.RemoveItem(e, ItemID.BOOK_SPELL_SUMMON);
                _unlockedGates.Add(CreatePortalEtahnia, "???");
                return new NormalDialogue(true, "This one is quite complicated, must be going to a quite far away place!", FacialExpression.SMILE, _knownName);
            }
            if (_currProgress == 1) return new NormalDialogue(true, "But it should be okay, if you want me to open it just speak to me again!", FacialExpression.SMILE, _knownName);

            PlayerController.S.SetIsCinematic(false);
            _current = Main;
            return null;
        }

        private void GetItem(ItemID id)
        {
            InventoryPopup.S.ForceCloseInventory();
            switch (id)
            {
                case ItemID.FOLDED_PAPER:
                    _current = GiveEtahnia;
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
                { GiveItem, "Discover portal" },
                { StartRandomConversation, "Speak" }
            };

            _randomConversations = new List<Func<EventDiscussion, int, IDialogueResult>>
            {
                RandomConversation1
            };

            _unlockedGates = new Dictionary<Func<EventDiscussion, int, IDialogueResult>, string>
            {
                { CreatePortalCancel, "Cancel" }
            };
        }
    }
}