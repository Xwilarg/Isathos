using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Spell", fileName = "Spell")]
    public class Spell : ScriptableObject
    {
        public GameObject ManaBall;
    }
}