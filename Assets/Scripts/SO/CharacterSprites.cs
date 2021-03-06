﻿using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/CharacterSprites", fileName = "CharacterSprites")]
    public class CharacterSprites : ScriptableObject
    {
        public Sprite up, down, left, right;

        public Sprite dead;
    }
}