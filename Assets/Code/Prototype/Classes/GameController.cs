using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    //TODO: Consider using an event system to make a reset callback on all objects that need to reset.
    [AddComponentMenu ("Controllers/Game Controller")]
    public class GameController : MonoBehaviour
    {
        public static GameStates CurrentState = GameStates.Start;
        public static Stats Stats = null;

        [SerializeField] private Stats _Stats = new Stats ();

        private void Awake ()
        {
			AssignReferences ();
			Setup ();
        }

		private void AssignReferences ()
		{
			var ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<UIController> ();
			_Stats.Init (ui);
			Stats = _Stats;
		}

		private void Setup ()
		{
			this.gameObject.tag = "GameController";
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

			// Store the current stats and reset them.
			_Stats.Store ();
        }

        private void OnDestroy ()
        {
            EventManager.OnStateSwitched -= UpdateState;
        }
    }

	[System.Serializable]
	public class Stats
	{
		public int Score { get { return _Score; } set { ChangeScore (value); } }
		public int MoveCount { get { return _MoveCount; } set { _MoveCount = value; } }
		public int BarrierHits { get { return _BarrierHits; } set { _BarrierHits = value; } }

		[SerializeField] private int _Score = 0;
		[SerializeField] private int _MoveCount = 0;
		[SerializeField] private int _BarrierHits = 0;

		private UIController _UI = null;

		public void Init (UIController ui)
		{
			_UI = ui;
		}

		public void Store ()
		{
			//TODO: Store stats after each match.

			Reset ();
		}

		private void Reset ()
		{
			_Score = 0;
			_MoveCount = 0;
			_BarrierHits = 0;
		}

		private void ChangeScore (int score)
		{
			_Score = score;

			_UI._GameScreen.UpdateScore (_Score);

			// If the score is a multiple of 5 or has hit 30. Increase player speed and cap it.
			if (score % 5 == 0 && _Score <= 30)
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ()._MovementSpeed += 50f;
		}
	}
}
