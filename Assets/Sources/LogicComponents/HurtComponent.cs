using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(HealthComponent), typeof(Actor))]
    public class HurtComponent : MonoBehaviour
    {
        private Actor _actor;
        private HealthComponent _health;

        private void Awake()
        {
            _actor = GetComponent<Actor>();
            _health = GetComponent<HealthComponent>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_health.IsLive == false) return;
            
            var weapon = other.GetComponent<WeaponComponent>();
            if (weapon is null) return;

            Debug.Log($"{weapon.GetComponentInParent<Actor>().name} deal {weapon.DamageValue} damage to {_health.gameObject.name}");
            _health.Damage(weapon.DamageValue);

            if (_health.IsLive == false)
            {
                var score = weapon.GetComponentInParent<ScoreComponent>();
                if (score is not null)
                {
                    var myScore = GetComponent<ScoreComponent>();
                    if (myScore is null) score.Kill(0);
                    else  score.Kill(myScore.Reward);
                }
            }
        }
    }
}