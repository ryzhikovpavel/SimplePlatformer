using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    public class ScoreComponent : MonoBehaviour
    {
        [SerializeField] private int reward;
        public int Value { get; private set; }
        public int Reward => reward;

        public void Kill(int score)
        {
            Value += score;
        }

        private void OnEnable()
        {
            Value = 0;
        }
    }
}