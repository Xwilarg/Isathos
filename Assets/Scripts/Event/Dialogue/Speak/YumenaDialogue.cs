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
    public class YumenaDialogue : ADialogue
    {
        public override IDialogueResult GetDialogue(EventDiscussion e, int lastChoiceId)
        {
            var result = _current(e, lastChoiceId);
            IncreaseProgress();
            return result;
        }

        private IDialogueResult Intro(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0)
            {
                _knownName = "Yumena";
                return new NormalDialogue(true, "Wh- What is happening? It's... It's okay Yumena, stays calm...", FacialExpression.NEUTRAL, _knownName);
            }
            if (_currProgress == 1) return new NormalDialogue(false, "Are you okay?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "I'm... Just not good with sudden situations... I need to sit down a bit...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(Character.HURIANE, true, "I'll guide her to her room so she can rest a bit.", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 4) return new NormalDialogue(true, "T-thanks...", FacialExpression.NEUTRAL, _knownName);
            e.gameObject.SetActive(false);
            InformationManager.S.DidSummonYumena = true;
            SummonManager.S.ClearSummon();
            _current = ChooseAction;
            return null;
        }

        private IDialogueResult ChooseAction(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(Character.NARRATOR, false, "This is Yumena room, what should I do?", FacialExpression.NEUTRAL, _knownName);

            if (lastChoiceId == -1) return new ChoiceDialogue(_dialogueChoice.Select(x => x.Value).ToArray());

            return AskQuestion(_dialogueChoice, e, lastChoiceId);
        }

        private IDialogueResult Cancel(EventDiscussion e, int lastChoiceId)
        {
            _current = ChooseAction;
            return null;
        }

        private IDialogueResult Speak(EventDiscussion e, int lastChoiceId)
        {
            _current = _nextQuestDialogue;
            return _current(e, lastChoiceId);
        }
        private IDialogueResult DontDisturb(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(Character.NARRATOR, true, "Yumena is resting, I shouldn't disturb her for now.", FacialExpression.NEUTRAL, _knownName);
            return null;
        }

        #region candyQuest

        private IDialogueResult IntroRoom(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "Yumena, are you okay?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 1) return new NormalDialogue(true, "...I just need to calm a bit, I'm not really good with stressful situations...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(false, "Is there something I can do?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 3) return new NormalDialogue(true, "Y-yeah, would you have something sweet?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 4) return new NormalDialogue(false, "Something sweet?", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 5) return new NormalDialogue(true, "It usually help to calm me down...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 6) return new NormalDialogue(false, "I will go look for it.", FacialExpression.NEUTRAL, _knownName);
            _nextQuestDialogue = IntroRoomFollowUp;
            _current = ChooseAction;
            _dialogueChoice.Add(GiveItem, "Give");
            return null;
        }

        private IDialogueResult IntroRoomFollowUp(EventDiscussion e, int lastChoiceId)
        {
            if (_currProgress == 0) return new NormalDialogue(Character.NARRATOR, false, "(Yumena looks a bit stressed, I should bring her what she need...)", FacialExpression.NEUTRAL, _knownName);
            _current = ChooseAction;
            return null;
        }


        private IDialogueResult GiveNothing(EventDiscussion e, int _)
        {
            if (_currProgress == 0) return new NormalDialogue(false, "Nevermind", FacialExpression.NEUTRAL, _knownName);

            PlayerController.S.SetIsCinematic(false);
            _current = ChooseAction;
            return null;
        }

        private IDialogueResult GiveCandy(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
            {
                EventManager.S.RemoveItem(e, ItemID.CANDY);
                return new NormalDialogue(true, "T-Thanks...", FacialExpression.NEUTRAL, _knownName);
            }
            if (_currProgress == 1) return new NormalDialogue(true, "...", FacialExpression.NEUTRAL, _knownName);
            if (_currProgress == 2) return new NormalDialogue(true, "I'm doing a bit better but I think I'll lie down for a bit...", FacialExpression.NEUTRAL, _knownName);

            _dialogueChoice.Remove(GiveItem);
            PlayerController.S.SetIsCinematic(false);
            _current = DontDisturb;
            return null;
        }

        private IDialogueResult GiveDefault(EventDiscussion e, int _)
        {
            if (_currProgress == 0)
                return new NormalDialogue(true, "I... I don't understand...", FacialExpression.NEUTRAL, _knownName);

            PlayerController.S.SetIsCinematic(false);
            _current = ChooseAction;
            return null;
        }

        private IDialogueResult GiveItem(EventDiscussion e, int lastChoiceId)
        {
            PlayerController.S.SetIsCinematic(true);
            InventoryPopup.S.ForceOpenInventory(GetItem);
            _currProgress = 0;
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

                case ItemID.CANDY:
                    _current = GiveCandy;
                    break;

                default:
                    _current = GiveDefault;
                    break;
            }
            EventManager.S.DisplayDiscution((EventDiscussion)PlayerController.S.GetEventTrigger().Event, -1, Character.YUMENA); // Force the display of next Etahnia dialogue
        }

        #endregion candyQuest

        private Dictionary<Func<EventDiscussion, int, IDialogueResult>, string> _dialogueChoice;
        private Func<EventDiscussion, int, IDialogueResult> _nextQuestDialogue;
        public YumenaDialogue() : base()
        {
            _current = Intro;
            _nextQuestDialogue = IntroRoom;

            _dialogueChoice = new Dictionary<Func<EventDiscussion, int, IDialogueResult>, string>
            {
                { Speak, "Speak" },
                { Cancel, "Leave" }
            };
        }
    }
}