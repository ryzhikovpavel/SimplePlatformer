using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(Actor))]
    public class AttackComponent : MonoBehaviour
    {
        private static readonly int AnimatorPropertyAttack = Animator.StringToHash("attack");
        private static readonly int AnimatorPropertyRandom = Animator.StringToHash("random");
        [SerializeField] private int powerPrice;
        
        private float _attackDelay;
        private Actor _actor;
        private EnergyComponent _energy;

        public bool IsAttack => _attackDelay > Time.time;

        public bool ToAttack()
        {
            if (enabled == false || IsAttack || _actor.IsDie) return false;
            if (_energy is not null)
            {
                if (powerPrice > _energy.Power) return false;
                _energy.Spend(powerPrice);
            }

            _attackDelay = Time.time + 0.5f;
            _actor.Animator.SetFloat(AnimatorPropertyRandom, Random.value);
            _actor.Animator.SetTrigger(AnimatorPropertyAttack);
            return true;
        }

        private void Awake()
        {
            _actor = GetComponent<Actor>();
            _energy = GetComponent<EnergyComponent>();
        }
    }
}