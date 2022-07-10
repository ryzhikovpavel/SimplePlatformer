using System;
using Sources.LogicComponents;
using UnityEngine;

namespace Sources
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int allyGroup;
        private Transform _transform;
        private HealthComponent _health;
        
        public int AllyGroup => allyGroup;
        public Animator Animator => animator;
        public World World { get; set; }
        public new Transform transform => _transform ? _transform : (_transform = GetComponent<Transform>());

        public bool IsLive => _health is not null && _health.IsLive;
        public bool IsDie => !IsLive;
        public float TimeDeath => _health == null ? 0 : _health.TimeDeath;

        public bool CanAttack() => IsLive;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }
    }
}