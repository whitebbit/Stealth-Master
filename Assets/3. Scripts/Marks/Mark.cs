using System;
using _3._Scripts.Marks.Enums;
using _3._Scripts.Units.Bots.Enums;
using UnityEngine;

namespace _3._Scripts.Marks
{
    [Serializable]
    public class Mark
    {
        [SerializeField] private MarkType type;
        [SerializeField] private Sprite sprite;

        public MarkType Type => type;

        public Sprite Sprite => sprite;
    }
}