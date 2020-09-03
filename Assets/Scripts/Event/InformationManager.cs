using UnityEngine;

namespace Event
{
    public class InformationManager : MonoBehaviour
    {
        public static InformationManager S { private set; get; }

        public void Awake()
        {
            S = this;
        }

        public bool HaveSealedPlanInfo { set; get; } = false; // Do you know that Etahnia was sealed away
        public bool HaveBanInfo { set; get; } = false; // Do you know that Etahnia was banned
        public bool DidReadGoblinBook { set; get; } = false; // Did you took the time to read the elven children story to Etahnia
    }
}
