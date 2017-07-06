using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    public class StatsUIController : MonoBehaviour
    {
        public void ReturnToMenu ()
        {
            EventManager.ChangeState (GameStates.Start);
        }
    }
}
