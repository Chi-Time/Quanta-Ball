using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private Text _ScoreLabel = null;

        //TODO: Consider using event for increasing score to de-couple code.
        public void UpdateScore (int score)
        {
            _ScoreLabel.text = "Score: " + score;
        }
    }
}
