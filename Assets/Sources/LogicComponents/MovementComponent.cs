using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Actor), typeof(GroundComponent))]
    public class MovementComponent : MonoBehaviour
    {
        private static readonly int AnimationPropertyName = Animator.StringToHash("move");
        [SerializeField] private float moveForce;

        private Rigidbody2D _body;
        private AttackComponent _attack;
        private Actor _actor;
        private int _movement;
        private GroundComponent _ground;
        private float _permanentVelocity;
        
        public bool SetMove(float direction)
        {
            if (_permanentVelocity != 0) return false;
            
            if (direction > 0)
            {
                _movement = 1;
                BeginMove();
                return true;
            }

            if (direction < 0)
            {
                _movement = -1;
                BeginMove();
                return true;
            }

            if (_ground.IsGrounded)
            {
                _movement = 0;
                StopMove();
            }
            return false;
        }

        public void SetPermanentVelocity(int direction, float velocity)
        {
            _permanentVelocity = velocity;
            _movement = direction;
        }
        
        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _attack = GetComponent<AttackComponent>();
            _actor = GetComponent<Actor>();
            _ground = GetComponent<GroundComponent>();
        }

        private bool CanMove()
        {
            if (_attack != null && _attack.IsAttack && _ground.IsGrounded) return false;
            if (_actor.IsDie) return false;
            return true;
        }
        
        private void FixedUpdate()
        {
            var v = _body.velocity;
            if (CanMove())
            {
                v.x = _movement * moveForce;
                if (v.x > 0 && v.x < _permanentVelocity) v.x = _permanentVelocity;
                if (v.x < 0 && v.x > _permanentVelocity) v.x = _permanentVelocity;
            }
            else
            {
                v.x = 0;
            }
            _body.velocity = v;
        }
        
        private void BeginMove()
        {
            _actor.Animator.SetInteger(AnimationPropertyName, 1);
            _actor.Animator.transform.localEulerAngles = new Vector3(0, _movement == 1 ? 0 : 180, 0);
        }

        private void StopMove()
        {
            _actor.Animator.SetInteger(AnimationPropertyName, 0);
            _movement = 0;
            FixedUpdate();
        }
    }
}