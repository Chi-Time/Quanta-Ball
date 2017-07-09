using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    //TODO: Consider using an event system to make a reset callback on all objects that need to reset.
    [AddComponentMenu ("Controllers/Game Controller")]
    public class GameController : MonoBehaviour
    {
        public int Score { get { return _Score; } set { ChangeScore (value); } }

        public static GameStates CurrentState = GameStates.Start;
        public static GameController Instance = null;

        [SerializeField] private int _Score = 0;
        private GameUIController _UIController = null;

        private void Awake ()
        {
            Instance = this;
            this.gameObject.tag = "GameController";
            _UIController = GameObject.FindGameObjectWithTag ("UI").GetComponent<GameUIController> ();

            EventManager.OnStateSwitched += UpdateState;
        }

        private void Start ()
        {
            EventManager.ChangeState (GameStates.Start);
        } 

        private void UpdateState (GameStates state)
        {
            CurrentState = state;

            switch(state)
            {
                case GameStates.Start:
                    Time.timeScale = 1.0f;
                    break;
                case GameStates.Settings:
                    Time.timeScale = 0.0f;
                    break;
                case GameStates.Stats:
                    Time.timeScale = 0.0f;
                    break;
                case GameStates.Game:
                    Time.timeScale = 1.0f;
                    break;
                case GameStates.Restart:
                    RestartGame ();
                    break;
                case GameStates.Pause:
                    Time.timeScale = 0.0f;
                    break;
                case GameStates.GameOver:
                    Time.timeScale = 1.0f;
                    break;
            }
        }

        private void RestartGame ()
        {
            // Grab the level generator
            var lg = GetComponent<LevelGenerator> ();
            lg.ResetLevel ();

            // Grab the player
            var p = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
            p.ResetBall ();

            // Reset score.
            Score = 0;
        }

        private void ChangeScore (int score)
        {
            _Score = score;

            // If the score is a multiple of 5 or has hit 30. Increase player speed and cap it.
            if(score % 5 == 0 && _Score <= 30)
                GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ()._MovementSpeed += 50f;
        }

        private void OnDestroy ()
        {
            EventManager.OnStateSwitched -= UpdateState;
        }
    }
}
