using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(HealthComponent))]
    public class BonusDropComponent: MonoBehaviour
    {
        [SerializeField] private Bonus.Types bonusType;
        [SerializeField, Range(0, 1)] private float chance;

        private HealthComponent _health;
        private bool _live;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            _live = true;
        }

        private void Update()
        {
            if (_health.IsDie && _live)
            {
                _live = false;
                if (chance > Random.value)
                    Game.World.DropHealPotion(transform.position);
            }
        }
    }
}