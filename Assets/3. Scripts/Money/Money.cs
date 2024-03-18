using System;
using _3._Scripts.Money.Enums;
using UnityEngine;

namespace _3._Scripts.Money
{
    public class Money : DroppableItem
    {
        [SerializeField] private MoneyType type;
        [SerializeField] private int value = 1;
        public int Value => value;
        public MoneyType Type => type;
        private bool taken;
        public void Take()
        {
            if (taken)
                throw new InvalidOperationException();

            taken = true;
        }
    }
}
