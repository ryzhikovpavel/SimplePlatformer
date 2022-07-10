using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenDefeat: UiScreen
    {
        private const string PrefsPlayerBestScore = "PlayerBestScore";
        
        [SerializeField] private TMP_Text textScore;
        [SerializeField] private TMP_Text textBestScore;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonExit;
        private CanvasGroup _canvas;

        public void Show(int score)
        {
            Activate();
            
            var bestScore = PlayerPrefs.GetInt(PrefsPlayerBestScore, 0);
            if (bestScore < score)
            {
                bestScore = score;
                PlayerPrefs.SetInt(PrefsPlayerBestScore, score);
                PlayerPrefs.Save();
            }

            textBestScore.text = $"Best Score: {bestScore}";
            textScore.text = $"Score: {score}";

            _canvas.alpha = 0;
            _canvas.DOFade(1, 3).SetEase(Ease.InCubic);
        }

        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            buttonRestart.onClick.AddListener(OnRestart);
            buttonExit.onClick.AddListener(OnExit);
        }

        private void OnExit()
        {
            Game.UI.Find<ScreenMenu>().Show();
        }

        private void OnRestart()
        {
            Game.UI.Find<ScreenGame>().Show();
        }
    }
}