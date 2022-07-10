using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(GroundComponent), typeof(Rigidbody2D), typeof(Actor))]
    public class JumpComponent : MonoBehaviour
    {
        private static readonly int AnimatorPropertyFly = Animator.StringToHash("fly");
        [SerializeField] private float jumpImpulse;

        private GroundComponent _ground;
        private Actor _actor;
        private Rigidbody2D _body;
        private SomersaultComponent _roll;
        private float _jumpDelay;

        public bool Jump()
        {
            if (enabled == false || _ground.IsGrounded == false) return false;
            if (_roll is not null && _roll.IsRollsOver) return false;
            if (_actor.IsDie || _jumpDelay > 0) return false;
            if (Math.Abs(_body.velocity.y) > 0.03f) return false;
            
            _body.AddForce(new Vector2(0, jumpImpulse), ForceMode2D.Impulse);
            _actor.Animator.SetInteger(AnimatorPropertyFly, 1);
            _jumpDelay = 0.2f;
            return true;
        }

        private void Awake()
        {
            _ground = GetComponent<GroundComponent>();
            _body = GetComponent<Rigidbody2D>();
            _actor = GetComponent<Actor>();
            _roll = GetComponent<SomersaultComponent>();
        }
        
        private void FixedUpdate()
        {
            var v = _body.velocity;            
            if (v.y < -0.02) _actor.Animator.SetInteger(AnimatorPropertyFly, -1);
            if (v.y > 0.02) _actor.Animator.SetInteger(AnimatorPropertyFly, 1);
            if (v.y > -0.02f && v.y < 0.02) _actor.Animator.SetInteger(AnimatorPropertyFly, 0);

            if (_jumpDelay > 0) _jumpDelay -= Time.fixedDeltaTime;
        }
    }
}