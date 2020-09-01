using System.Collections.Generic;
using UnityEngine;

namespace Achievement
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager S { private set; get; }

        private Dictionary<AchievementID, Achievement> _achievements;

        [SerializeField]
        private GameObject _gameObject;

        public void Awake()
        {
            S = this;
        }

        public void DisplayAchievement(AchievementID id)
        {
            var go = Instantiate(_gameObject, transform);

            _achievements = new Dictionary<AchievementID, Achievement>();
        }
    }
}