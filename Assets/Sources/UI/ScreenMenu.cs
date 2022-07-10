using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI
{
    public class ScreenMenu : UiScreen
    {
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonExit;
        
        public void Show()
        {
            Activate();
        }
        
        private void Start()
        {
            buttonExit.onClick.AddListener(OnExit);
            buttonPlay.onClick.AddListener(OnPlay);
            
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                buttonExit.interactable = false;
        }

        private void OnPlay()
        {
            Game.UI.Find<ScreenGame>().Show();
        }

        private void OnExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}