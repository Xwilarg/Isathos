using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Reaction", fileName = "Reaction")]
    public class Reaction : ScriptableObject
    {
        public GameObject relationUp;
        public GameObject relationDown;

        public GameObject newItem;
        public GameObject removeItem;
        public GameObject blood;
    }
}