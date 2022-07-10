using System;
using UnityEngine;

namespace Sources.LogicComponents
{
    [RequireComponent(typeof(MovementComponent), typeof(AttackComponent), typeof(Actor))]
    public class EnemyComponent : MonoBehaviour
    {
        [SerializeField] private float locatorDelaySeconds;
        [SerializeField] private float minDistanceForMoveTargetSqr;
        [SerializeField] private float minDistanceForAttackTargetSqr;
        
        private MovementComponent _move;
        private AttackComponent _attack;
        private Actor _actor;
        private Actor _target;
        private float _targetLocatorDelay;

        private void Awake()
        {
            _move = GetComponent<MovementComponent>();
            _attack = GetComponent<AttackComponent>();
            _actor = GetComponent<Actor>();
        }

        private void OnEnable()
        {
            _targetLocatorDelay = 1;
        }

        private void Update()
        {
            _targetLocatorDelay -= Time.deltaTime;

            if (_targetLocatorDelay <= 0)
            {
                _targetLocatorDelay = locatorDelaySeconds;
                FindTarget(out _target);
            }

            if (ValidateTarget(_target))
            {
                if (MoveToTarget() == false) Attack();
            }
            else _move.SetMove(0);
        }

        private bool MoveToTarget()
        {
            var targetPosition = _target.transform.position;
            var myPosition = _actor.transform.position;
            var distance = (targetPosition - myPosition).sqrMagnitude;
            if (distance < minDistanceForAttackTargetSqr)
            {
                _move.SetMove(0);
                return false;
            }

            _move.SetMove(targetPosition.x - myPosition.x);
            return true;
        }

        private void Attack()
        {
            if (_attack.IsAttack == false) _attack.ToAttack();
        }

        private bool ValidateTarget(Actor target)
        {
            if (target is null) return false;
            if (target.CanAttack() == false) return false;
            if (Math.Abs(target.transform.position.y - _actor.transform.position.y) > 0.5f) return false;
            return true;
        }
        
        private bool FindTarget(out Actor target)
        {
            var distance = float.MaxValue;
            var position = _actor.transform.position;
            target = null;
            foreach (var actor in _actor.World.Actors)
            {
                if (actor.AllyGroup == _actor.AllyGroup || ValidateTarget(actor) == false) continue;

                var dis = (actor.transform.position - position).sqrMagnitude;
                if (dis > distance || dis > minDistanceForMoveTargetSqr) continue;

                target = actor;
                distance = dis;
            }

            return target is not null;
        }
    }
}