using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class PauseUIController : MonoBehaviour
    {
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
