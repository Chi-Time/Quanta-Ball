using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class GameUIController : MonoBehaviour
    {
        public enum UIStates { Start, Game, Pause, GameOver };
        public UIStates CurrentUIState;

        public StartUIController _StartScreen = null;
        public InGameUIController _GameScreen = null;
        public PauseUIController _PauseScreen = null;
        public GameOverUIController _GameOverScreen = null;

        void Awake ()
        {
            Setup ();
        }

        void Setup ()
        {
            UpdateState (UIStates.Start);
        }

        public void UpdateState (UIStates stateToActivate)
        {
            CurrentUIState = stateToActivate;

            switch (stateToActivate)
            {
                case UIStates.Start:
                    DisplayScreen (_StartScreen.gameObject);
                    break;
                case UIStates.Game:
                    DisplayScreen (_GameScreen.gameObject);
                    break;
                case UIStates.Pause:
                    DisplayScreen (_PauseScreen.gameObject);
                    break;
                case UIStates.GameOver:
                    DisplayScreen (_GameOverScreen.gameObject);
                    break;
            }
        }

        void DisplayScreen (GameObject screenToDisplay)
        {
            _StartScreen.gameObject.SetActive (false);
            _GameScreen.gameObject.SetActive (false);
            _PauseScreen.gameObject.SetActive (false);
            _GameOverScreen.gameObject.SetActive (false);

            screenToDisplay.SetActive (true);
        }
    }
}
