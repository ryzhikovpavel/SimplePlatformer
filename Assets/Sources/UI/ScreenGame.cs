using System;
using Sources.LogicComponents;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI
{
    public class ScreenGame : UiScreen
    {
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderPower;
        [SerializeField] private TMP_Text textScore;

        [Header("Mobile Input Controls")]
        [SerializeField] private Button buttonLeft;
        [SerializeField] private Button buttonRight;
        [SerializeField] private Button buttonJump;
        [SerializeField] private Button buttonRoll;
        [SerializeField] private Button buttonAttack;

        private HealthComponent _playerHealth;
        private ScoreComponent _playerScore;
        private EnergyComponent _playerEnergy;

        private int _lastScore;
        private int _lastHealth;
        private int _lastPower;

        public void Show()
        {
            Activate();
            Game.World.StartNewGame();
            
            _playerHealth = Game.World.MainPlayer.GetComponent<HealthComponent>();
            _playerScore = Game.World.MainPlayer.GetComponent<ScoreComponent>();
            _playerEnergy = Game.World.MainPlayer.GetComponent<EnergyComponent>();
            textScore.gameObject.SetActive(_playerScore != null);
            _lastScore = -1;
            _lastHealth = -1;
            _lastPower = -1;
            UpdateScore();
            UpdateHealth();
        }

        private void Start()
        {
            Game.World.EventGameFinished += WorldOnEventGameFinished;
            
            buttonJump.onClick.AddListener(()=>Game.MobileInput.FireJump());
            buttonAttack.onClick.AddListener(()=>Game.MobileInput.FireAttack());
            buttonRoll.onClick.AddListener(()=>Game.MobileInput.FireRoll());

            bool isMobile = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
            
            buttonAttack.gameObject.SetActive(isMobile);
            buttonJump.gameObject.SetActive(isMobile);
            buttonRoll.gameObject.SetActive(isMobile);
            buttonLeft.gameObject.SetActive(isMobile);
            buttonRight.gameObject.SetActive(isMobile);
        }

        private void WorldOnEventGameFinished()
        {
            Game.UI.Find<ScreenDefeat>().Show(_playerScore.Value);
        }

        private void Update()
        {
            UpdateHealth();
            UpdateScore();
            UpdatePower();
        }

        private void UpdatePower()
        {
            if (_playerEnergy is not null &&_lastPower != _playerEnergy.Power)
            {
                sliderPower.value = _playerEnergy.Power / (float)_playerEnergy.MaxPower;
                _lastPower = _playerEnergy.Power;
            } 
        }

        private void UpdateHealth()
        {
            if (_playerHealth is not null && _lastHealth != _playerHealth.Value)
            {
                _lastHealth = _playerHealth.Value;
                sliderHealth.value = _playerHealth.Value / (float)_playerHealth.MaxValue;
            }
        }

        private void UpdateScore()
        {
            if (_playerScore is not null && _lastScore != _playerScore.Value)
            {
                _lastScore = _playerScore.Value;
                textScore.text = $"Score: {_playerScore.Value}";
            }
        }
    }
}