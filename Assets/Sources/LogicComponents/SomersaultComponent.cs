using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(GroundComponent), typeof(Actor), typeof(MovementComponent))]
    public class SomersaultComponent: MonoBehaviour
    {
        private static readonly int AnimatorPropertyMove = Animator.StringToHash("move");
        [SerializeField] private float force;
        
        private GroundComponent _ground;
        private MovementComponent _move;
        private Actor _actor;
        private int _direction;
        private float _time;

        public bool IsRollsOver => _time > 0;
        
        public bool Roll()
        {
            if (_ground.IsGrounded == false || _actor.IsDie) return false;
            if (_time > 0) return false;

            _time = 0.5f;
            _actor.Animator.SetInteger(AnimatorPropertyMove, 2);
            if (_actor.Animator.transform.localEulerAngles.y == 0) _direction = 1;
            else _direction = -1;
            Update();
            return true;
        }

        private void Update()
        {
            if (IsRollsOver == false) return;
            _move.SetPermanentVelocity(_direction, force * _direction);
            _time -= Time.deltaTime;

            if (_time < 0)
            {
                _actor.Animator.SetInteger(AnimatorPropertyMove, 0);
                _move.SetPermanentVelocity(0, 0);
            }
        }

        private void Awake()
        {
            _ground = GetComponent<GroundComponent>();
            _actor = GetComponent<Actor>();
            _move = GetComponent<MovementComponent>();
        }
    }
}