using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Controllers/Game Controller")]
    public class GameController : MonoBehaviour
    {
        public int Score { get { return _Score; } set { ChangeScore (value); } }

        public static GameController Instance = null;

        [SerializeField] private int _Score = 0;

        private void Awake ()
        {
            Instance = this;
            this.gameObject.tag = "GameController";
        }

        private void ChangeScore (int score)
        {
            _Score = score;

            if(score % 5 == 0)
            {
                //TODO: Change player's movement speed.
            }
        }
    }
}
