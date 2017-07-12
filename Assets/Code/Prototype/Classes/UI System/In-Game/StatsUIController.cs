using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class StatsUIController : MonoBehaviour
    {
        [SerializeField] private Text MoveCountLabel = null;
        [SerializeField] private Text TimePlayedLabel = null;
        [SerializeField] private Text BarrierHitsLabel = null;

        public void ReturnToMenu ()
        {
            EventManager.ChangeState (GameStates.Start);
        }
    }
}
