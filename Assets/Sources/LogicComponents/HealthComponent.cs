using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(Actor))]
    public class HealthComponent: MonoBehaviour
    {
        private static readonly int AnimatorPropertyDie = Animator.StringToHash("die");
        private static readonly int AnimatorPropertyHurt = Animator.StringToHash("hurt");

        [SerializeField] private AudioClip sfxDamaged;
        [SerializeField] private AudioClip sfxDie;
        [SerializeField] private int health;
        private int _spawnValue;
        private Actor _actor;

        public bool IsLive => Value > 0;
        public bool IsDie => !IsLive;
        public int Value => health;
        public int MaxValue => _spawnValue;
        public float TimeDeath { get; private set; }

        public void Damage(int damageValue)
        {
            if (enabled == false || Value <= 0) return;
            health -= damageValue;
            
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            else
            {
                _actor.Animator.SetTrigger(AnimatorPropertyHurt);
                sfxDamaged.Play();
            }
        }

        public void Heal(int value)
        {
            if (enabled == false || Value <= 0) return;
            health += value;
        }

        private void Die()
        {
            _actor.Animator.SetTrigger(AnimatorPropertyDie);
            TimeDeath = Time.time;
            sfxDie.Play();
        }

        private void Awake()
        {
            _actor = GetComponent<Actor>();
            _spawnValue = health;
        }

        private void OnEnable()
        {
            health = _spawnValue;
            TimeDeath = 0;
        }
    }
}