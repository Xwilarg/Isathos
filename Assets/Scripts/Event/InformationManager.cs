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

        public bool HaveSealedPlanInfo { set; get; } = false;
        public bool HaveBanInfo { set; get; } = false;
    }
}
