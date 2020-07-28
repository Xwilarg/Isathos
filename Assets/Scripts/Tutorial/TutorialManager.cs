using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager S;

    private void Awake()
    {
        S = this;
    }

    private void Start()
    {
        _progression = TutorialProgression.BEGINNING;
    }

    private TutorialProgression _progression;

    public TutorialProgression GetProgression()
        => _progression;

    /// ALL THE FOLLOWING FUNCTIONS INCREASE THE TUTORIAL PROGRESSION

    /// <summary>
    /// Triggered when the player first speak and finish the introduction dialogue
    /// </summary>
    public void IntroduceEtahnia()
    {
        if (_progression == TutorialProgression.BEGINNING)
            _progression = TutorialProgression.ETAHNIA_INTRO;
    }

    /// <summary>
    /// Triggered when the player first go back to the human world and is told that he must kill Etahnia
    /// </summary>
    public void IntroduceKillEtahnia()
    {
        if (_progression == TutorialProgression.ETAHNIA_INTRO)
            _progression = TutorialProgression.ETAHNIA_KILL_INTRO;
    }

    public void IntroduceNextEtahniaStep()
    {
        if (_progression == TutorialProgression.ETAHNIA_KILL_INTRO)
            _progression = TutorialProgression.ETAHNIA_DECIDE_NEXT_STEP;
    }
}
