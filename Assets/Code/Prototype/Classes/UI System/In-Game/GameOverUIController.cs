using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class GameOverUIController : MonoBehaviour
    {
        public void RestartGame ()
        {
            EventManager.ChangeState (GameStates.Game);
        }

        public void Menu ()
        {
            EventManager.ChangeState (GameStates.Start);
        }

        public void Quit ()
        {
            EventManager.ChangeState (GameStates.Pause);
        } 
    }
}
