using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class InGameUIController : MonoBehaviour
    {
        public static InGameUIController Instance = null;

        public GameUIController _GameScreen = null;
        public PauseUIController _PauseScreen = null;
        public GameOverUIController _GameOverScreen = null;

        private void Awake ()
        {
        }

        private void OnUISwitch ()
        {
        }

        private void SwitchScreen (GameObject screen)
        {
        }
    }
}
