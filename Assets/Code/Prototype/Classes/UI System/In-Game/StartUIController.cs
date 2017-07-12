using UnityEngine;

namespace Assets.Code.Prototype.Classes
{
    public class StartUIController : MonoBehaviour
    {
        public void StartGame ()
        {
            EventManager.ChangeState (GameStates.Game);
        }

        public void ShowStats ()
        {
            EventManager.ChangeState (GameStates.Stats);
        }

        public void Exit ()
        {
            Application.Quit ();
        } 
    }
}
