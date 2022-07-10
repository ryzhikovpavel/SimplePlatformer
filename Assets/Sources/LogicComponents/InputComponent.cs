using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.LogicComponents
{
    public class InputComponent : MonoBehaviour
    {
        private const string HorizontalAxisName = "Horizontal";
        private MovementComponent _movement;
        private JumpComponent _jump;
        private AttackComponent _attack;
        private SomersaultComponent _roll;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _jump = GetComponent<JumpComponent>();
            _attack = GetComponent<AttackComponent>();
            _roll = GetComponent<SomersaultComponent>();
        }

        private void Update()
        {
            if (_movement is not null)
            {
                if (Game.MobileInput.HorizontalAxis == 0)
                    _movement.SetMove(Input.GetAxis(HorizontalAxisName));
                else 
                    _movement.SetMove(Game.MobileInput.HorizontalAxis);
            }
            
            if (_jump is not null && (CheckAxisWithoutMobile("Jump") || Game.MobileInput.Jump))
                _jump.Jump();

            if (_attack is not null && (CheckAxisWithoutMobile("Fire1") || Game.MobileInput.Attack))
                _attack.ToAttack();

            if (_roll is not null && (CheckAxisWithoutMobile("Roll") || Game.MobileInput.Roll))
                _roll.Roll();
        }

        private bool CheckAxisWithoutMobile(string axis)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    return false;
                default:
                    return Input.GetAxis(axis) >= 1;
            }
        }
    }
}