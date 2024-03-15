using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Marks.Enums;
using UnityEngine;

namespace _3._Scripts.Marks
{
    public class MarkSystem : MonoBehaviour
    {
        [SerializeField] private List<Mark> marks = new();
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetMark(MarkType.None);
        }

        public void SetMark(MarkType type)
        {
            spriteRenderer.sprite = marks.FirstOrDefault(m => m.Type == type)?.Sprite;
        }
    }
}