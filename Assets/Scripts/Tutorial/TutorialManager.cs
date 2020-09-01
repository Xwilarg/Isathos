using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager S;

        private void Awake()
        {
            S = this;
        }

        private void Start()
        {
            _progression = Progression.BEGINNING;
        }

        private Progression _progression;

        public Progression GetProgression()
            => _progression;

        /// ALL THE FOLLOWING FUNCTIONS INCREASE THE TUTORIAL PROGRESSION

        /// <summary>
        /// Triggered when the player first speak and finish the introduction dialogue
        /// </summary>
        public void IntroduceEtahnia()
        {
            if (_progression == Progression.BEGINNING)
                _progression = Progression.ETAHNIA_INTRO;
        }

        /// <summary>
        /// Triggered when the player first go back to the human world and is told that he must kill Etahnia
        /// </summary>
        public void IntroduceKillEtahnia()
        {
            if (_progression == Progression.ETAHNIA_INTRO)
                _progression = Progression.ETAHNIA_KILL_INTRO;
        }

        public void IntroduceNextEtahniaStep()
        {
            if (_progression == Progression.ETAHNIA_KILL_INTRO)
                _progression = Progression.ETAHNIA_DECIDE_NEXT_STEP;
        }
    }
}