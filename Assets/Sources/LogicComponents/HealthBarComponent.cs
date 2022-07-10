using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(HealthComponent))]
    public class HealthBarComponent: MonoBehaviour
    {
        [SerializeField] private GameObject lineObjectsGroup;
        [SerializeField] private Transform healthLine; 
        private HealthComponent _health;
        private int _lastHealth;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            Clear();
            _lastHealth = -1;
        }

        private void Update()
        {
            if (_health.IsDie)
            {
                if (_lastHealth > 0) Clear();
                _lastHealth = 0;
                return;
            }

            if (_lastHealth != _health.Value)
            {
                _lastHealth = _health.Value;
                healthLine.localScale = new Vector3(_health.Value / (float)_health.MaxValue, 1, 1);
                lineObjectsGroup.SetActive(_health.Value != _health.MaxValue);
            }
        }

        private void Clear()
        {
            lineObjectsGroup.SetActive(false);
        }
    }
}