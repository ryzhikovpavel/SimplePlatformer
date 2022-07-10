using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    public class EnergyComponent : MonoBehaviour
    {
        [SerializeField] private float recoveryDelay;
        [SerializeField] private int power;
        private int _spawnPower;
        private float _timeDelay;

        public int Power => power;
        public int MaxPower => _spawnPower;

        public void Spend(int value)
        {
            if (power == 0) return;
            power -= value;
            if (power < 0) power = 0;
        }
        
        private void Awake()
        {
            _spawnPower = power;
        }

        private void OnEnable()
        {
            power = _spawnPower;
            _timeDelay = recoveryDelay;
        }

        private void Update()
        {
            if (power == _spawnPower) return;
            _timeDelay -= Time.deltaTime;
            if (_timeDelay <= 0)
            {
                _timeDelay += recoveryDelay;
                power++;
            }
        }
    }
}