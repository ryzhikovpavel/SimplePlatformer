using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    public class BonusTakerComponent : MonoBehaviour
    {
        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var bonus = other.GetComponent<Bonus>();
            if (bonus is null) return;

            switch (bonus.Type)
            {
                case Bonus.Types.HealPotion:
                    if (TakeHealPotion(bonus) == false) return;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            bonus.Take();
        }
        
        private bool TakeHealPotion(Bonus bonus)
        {
            if (_health is null) return false;
            if (_health.Value == _health.MaxValue) return false;

            _health.Heal(bonus.Value);
            return true;
        }
    }
}