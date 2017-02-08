using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    //TODO: Consider using an event system to make a reset callback on all objects that need to reset.
    [AddComponentMenu ("Controllers/Game Controller")]
    public class GameController : MonoBehaviour
    {
        public int Score { get { return _Score; } set { ChangeScore (value); } }

        public static GameController Instance = null;

        public bool IsGameOver = false;

        [SerializeField] private int _Score = 0;

        private void Awake ()
        {
            Instance = this;
            this.gameObject.tag = "GameController";
        }

        private void ChangeScore (int score)
        {
            _Score = score;

            if(score % 5 == 0 && _Score <= 30)
                GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ()._MovementSpeed += 50f;
        }
    }
}
