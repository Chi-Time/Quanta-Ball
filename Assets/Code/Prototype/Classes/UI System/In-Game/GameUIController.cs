﻿using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class GameUIController : MonoBehaviour
    {
        public StartUIController _StartScreen = null;
        public SettingsUIController _SettingsScreen = null;
        public InGameUIController _GameScreen = null;
        public PauseUIController _PauseScreen = null;
        public GameOverUIController _GameOverScreen = null;
        public StatsUIController _StatsScreen = null;

        private void Awake ()
        {
            Setup ();
        }

        private void Setup ()
        {
            EventManager.OnStateSwitched += UpdateState;
        }

        public void UpdateState (GameStates stateToActivate)
        {
            switch (stateToActivate)
            {
                case GameStates.Start:
                    DisplayScreen (_StartScreen.gameObject);
                    break;
                case GameStates.Settings:
                    DisplayScreen (_SettingsScreen.gameObject);
                    break;
                case GameStates.Stats:
                    DisplayScreen (_StatsScreen.gameObject);
                    break;
                case GameStates.Game:
                    DisplayScreen (_GameScreen.gameObject);
                    break;
                case GameStates.Pause:
                    DisplayScreen (_PauseScreen.gameObject);
                    break;
                case GameStates.GameOver:
                    DisplayScreen (_GameOverScreen.gameObject);
                    break;
            }
        }

        private void DisplayScreen (GameObject screenToDisplay)
        {
            _StartScreen.gameObject.SetActive (false);
            _SettingsScreen.SetActive (false);
            _StatsScreen.SetActive (false);
            _GameScreen.gameObject.SetActive (false);
            _PauseScreen.gameObject.SetActive (false);
            _GameOverScreen.gameObject.SetActive (false);

            screenToDisplay.SetActive (true);
        }

        private void OnDestroy ()
        {
            EventManager.OnStateSwitched -= UpdateState;
        }
    }
}
