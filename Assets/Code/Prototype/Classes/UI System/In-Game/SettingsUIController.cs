using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    public class SettingsUIController : MonoBehaviour
    {
        //TODO: Options settings.
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
