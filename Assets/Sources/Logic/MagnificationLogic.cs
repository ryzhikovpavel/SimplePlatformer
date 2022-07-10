using Sources.LogicComponents;
using UnityEngine;

namespace Sources
{
    public class MagnificationLogic: MonoBehaviour
    {
        [SerializeField] private float delay;
        private float _delayToUpgrade;
        private int _bonusDamage;

        private void Awake()
        {
            Game.World.EventGameStarted += WorldOnEventGameStarted;
            Game.World.EventSpawnActor += SetBonusDamage;
            enabled = false;
        }

        private void WorldOnEventGameStarted()
        {
            _delayToUpgrade = delay;
            _bonusDamage = 0;
            enabled = true;
        }

        private void SetBonusDamage(Actor actor)
        {
            var weapons= actor.GetComponentsInChildren<WeaponComponent>(true);
            foreach (var weapon in weapons)
            {
                weapon.BonusDamage = _bonusDamage;
            }
        }

        private void Update()
        {
            _delayToUpgrade -= Time.deltaTime;
            if (_delayToUpgrade < 0)
            {
                _delayToUpgrade = delay;
                Upgrade();
            }
        }

        private void Upgrade()
        {
            _bonusDamage++;
            Debug.Log("Upgrade bonus damage to " + _bonusDamage);

            foreach (var actor in Game.World.Actors)
            {
                if (actor == Game.World.MainPlayer) continue;
                SetBonusDamage(actor);
            }
        }
    }
}