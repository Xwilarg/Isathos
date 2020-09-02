using Event.Dialogue.Speak;
using Event.Dialogue.Result;
using Event.Look;
using Event.Trigger;
using Event.Trigger.EventType;
using Inventory;
using Other;
using Player;
using System;
using Tutorial;
using UnityEngine;

namespace Event
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager S;

        [SerializeField]
        private SO.Reaction _reaction;

        [SerializeField]
        private GameObject _forestEntrance;

        [SerializeField]
        private BoxCollider2D _eranelDoor;

        public void EnableEranelDoor() // TUTORIAL
            => _eranelDoor.enabled = true;

        // CHARACTERS
        private EtahniaDialogue _etahnia = new EtahniaDialogue(); public EtahniaDialogue GetEtahnia() => _etahnia;
        private AnaelDialogue _anael = new AnaelDialogue();
        private SalenaeDialogue _salenae = new SalenaeDialogue();
        private UnarDialogue _unar = new UnarDialogue();
        private NachiDialogue _nachi = new NachiDialogue();
        private EranelDialogue _eranel = new EranelDialogue();

        // TUTORIAL
        private TutorialLook _tutorial = new TutorialLook();
        private TutorialDialogue _tutorialD = new TutorialDialogue();

        // MAP
        private InvocationHouseLook _invocationHouse = new InvocationHouseLook();
        private EranelHouseLook _eranelHouse = new EranelHouseLook();

        // GAME OVER
        private GameOverDialogue _gameOver = new GameOverDialogue();

        private void Awake()
        {
            S = this;
        }

        public void Clear()
        {
            _etahnia.Clear();
            _anael.Clear();
            _salenae.Clear();
            _nachi.Clear();
            _unar.Clear();
            _eranel.Clear();

            _tutorial.Clear();
            _tutorialD.Clear();

            _invocationHouse.Clear();
            _eranelHouse.Clear();
        }

        public void DisplayNewItem(ItemID id)
        {
            GameObject go = Instantiate(_reaction.newItem, PlayerController.S.transform.position + (Vector3)(Vector2.one * .2f), Quaternion.identity);
            ItemManager.S.GetItem(id).InitItemPopup(go.GetComponent<NewItem>());
            PlayerController.S.Inventory.AddItem(id);
        }

        public void RemoveItem(EventDiscussion e, ItemID id)
        {
            GameObject go = Instantiate(_reaction.removeItem, e.transform.position + (Vector3)(Vector2.one * .2f), Quaternion.identity);
            ItemManager.S.GetItem(id).InitItemPopup(go.GetComponent<NewItem>());
            PlayerController.S.Inventory.RemoveItem(id);
        }

        public void DisplayGameOver(EventDiscussion e, bool withBlood)
        {
            PlayerController.S.Loose();
            if (withBlood)
                Instantiate(_reaction.blood, e.transform.position, Quaternion.identity);
        }

        public void DisplayReaction(EventDiscussion e, ReactionType react)
        {
            GameObject go = null;
            if (react == ReactionType.RELATION_UP)
                go = _reaction.relationUp;
            else if (react == ReactionType.RELATION_DOWN)
                go = _reaction.relationDown;

            if (go == null)
                throw new ArgumentException("Invalid reaction " + react.ToString());

            Instantiate(go, e.transform.position + (Vector3)(Vector2.one * .2f), Quaternion.identity);
        }

        public void StartEvent(EventTrigger e, Transform player)
        {
            if (e.Event is EventDiscussion eDisc)
            {
                StartDiscussion(eDisc, -1);
            }
            else if (e.Event is EventDoor eDoor)
            {
                if (eDoor.RequiredPhase > TutorialManager.S.GetProgression()) // We can't use that because we didn't go far enough in the tutorial
                {
                    var result = _tutorial.GetText(null);
                    if (result == null)
                    {
                        Clear();
                        DialoguePopup.S.Close();
                        return;
                    }
                    StartPopup(result);
                }
                else if (eDoor.FailureType == DoorFailureType.NONE)
                {
                    player.position = eDoor.Destination.transform.position;
                    if (!eDoor.IsWarp)
                    {
                        if (player.position.y > eDoor.transform.position.y)
                            player.position += Vector3.down * .5f;
                        else
                            player.position += Vector3.up * .5f;
                    }
                    player.localScale = new Vector3(eDoor.OutputScale, eDoor.OutputScale, 1f);
                    eDoor.transform.parent.gameObject.SetActive(false);
                    eDoor.Destination.transform.parent.gameObject.SetActive(true);
                    switch (TutorialManager.S.GetProgression())
                    {
                        case Progression.ETAHNIA_INTRO: // The player go back to the human world for the first time
                            PlayerController.S.SetCanMove(false);
                            Clear();
                            StartTutorialDiscution();
                            eDoor.Destination = _forestEntrance;
                            eDoor.RequiredPhase = Progression.ETAHNIA_DECIDE_NEXT_STEP;
                            eDoor.OutputScale = 0.8f;
                            break;

                        case Progression.ETAHNIA_KILL_INTRO:
                            _etahnia.UpdateTutorial();
                            break;

                        case Progression.ETAHNIA_DECIDE_NEXT_STEP:
                            break;
                    }
                }
            }
            else if (e.Event is EventLook eLook)
            {
                string result;
                if (eLook.Zone == Zone.INVOCATION_HOUSE)
                    result = _invocationHouse.GetText(eLook.ObjectId);
                else if (eLook.Zone == Zone.ERANEL_HOUSE)
                    result = _eranelHouse.GetText(eLook.ObjectId);
                else
                    throw new ArgumentException("Invalid zone " + eLook.Zone);
                if (result == null)
                {
                    Clear();
                    DialoguePopup.S.Close();
                    return;
                }
                StartPopup(result);
            }
            else
                throw new ArgumentException("Invalid event " + e.name);
        }

        #region dialogue
        private void StartPopup(string text)
        {
            DialoguePopup.S.Display(Character.MC, Character.NONE, FacialExpression.NEUTRAL, FacialExpression.NEUTRAL, text, true, "Me");
        }

        public void StartGameOverDiscution(int id = -1)
        {
            var result = (NormalDialogue)_gameOver.Defeat(null, -1);
            if (result.IsSpeaking)
                _speakerTwoLastExpression = result.Expression;
            else
                _speakerOneLastExpression = result.Expression;
            DialoguePopup.S.Display(Character.MC, Character.EXPL_GOD, _speakerOneLastExpression, _speakerTwoLastExpression, result.Text, !result.IsSpeaking, result.NameOverride);
        }

        public void StartTutorialDiscution(int id = -1)
        {
            IDialogueResult result;
            result = _tutorialD.GetDialogue(null, id);
            if (result == null)
            {
                PlayerController.S.SetCanMove(true);
                Clear();
                DialoguePopup.S.Close();
                return;
            }
            if (result is NormalDialogue nDial)
            {
                if (nDial.IsSpeaking)
                    _speakerTwoLastExpression = nDial.Expression;
                else
                    _speakerOneLastExpression = nDial.Expression;
                DialoguePopup.S.Display(Character.MC, nDial.Speaker, _speakerOneLastExpression, _speakerTwoLastExpression, nDial.Text, !nDial.IsSpeaking, nDial.NameOverride);
            }
            else if (result is ChoiceDialogue cDial)
            {
                DialoguePopup.S.DisplayChoices(null, cDial.Choices);
            }
        }

        public void StartDiscussion(EventDiscussion e, int id = -1)
        {
            // Tutorial choice callback
            if (e == null)
            {
                StartTutorialDiscution(id);
                return;
            }

            Character c = e.GetCharacter();
            DisplayDiscution(e, id, c);
        }

        public void DisplayDiscution(EventDiscussion e, int id, Character c)
        {

            IDialogueResult result;
            if (c == Character.ETAHNIA)
                result = _etahnia.GetDialogue(e, id);
            else if (c == Character.ANAEL)
                result = _anael.GetDialogue(e, id);
            else if (c == Character.SALENAE)
                result = _salenae.GetDialogue(e, id);
            else if (c == Character.UNAR)
                result = _unar.GetDialogue(e, id);
            else if (c == Character.NACHI)
                result = _nachi.GetDialogue(e, id);
            else if (c == Character.ERANEL)
                result = _eranel.GetDialogue(e, id);
            else
                throw new ArgumentException("Invalid character " + c.ToString());
            if (result == null)
            {
                Clear();
                DialoguePopup.S.Close();
                return;
            }
            if (result is NormalDialogue nDial)
            {
                if (nDial.IsSpeaking)
                    _speakerTwoLastExpression = nDial.Expression;
                else
                    _speakerOneLastExpression = nDial.Expression;
                DialoguePopup.S.Display(Character.MC, nDial.Speaker == Character.NONE ? c : nDial.Speaker, _speakerOneLastExpression, _speakerTwoLastExpression, nDial.Text, !nDial.IsSpeaking, nDial.NameOverride);
            }
            else if (result is ChoiceDialogue cDial)
            {
                DialoguePopup.S.DisplayChoices(e, cDial.Choices);
            }
            else
                throw new InvalidOperationException("GetDialogue returned an unknown type for " + c.ToString());
        }

        // To keep the last expression when the other person is speaking
        private FacialExpression _speakerOneLastExpression = FacialExpression.NEUTRAL;
        private FacialExpression _speakerTwoLastExpression = FacialExpression.SMILE;
        #endregion dialogue
    }
}