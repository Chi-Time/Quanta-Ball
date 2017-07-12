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

        public void MuteMusic (bool isMuted)
        {
            if (isMuted)
                print ("Music Muted: TBA");
            else
                print ("Music Unmuted: TBA");
        }

        public void MuteAudio (bool isMuted)
        {
            if (isMuted)
                print ("Audio Muted: TBA");
            else
                print ("Audio Unmuted: TBA");
        }
    }
}
