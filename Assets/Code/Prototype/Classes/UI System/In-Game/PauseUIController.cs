using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class PauseUIController : MonoBehaviour
    {
        public void Unpause ()
        {
            EventManager.ChangeState (GameStates.Game);
        }
    }
}
