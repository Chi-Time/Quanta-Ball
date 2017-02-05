using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Prototype.Classes
{
    public class MainMenuUIController : MonoBehaviour
    {
        public static MainMenuUIController Instance = null;

        public MenuUIController _MenuController = null;
        public OptionsUIController _OptionsController = null;
        public CreditsUIController _CreditsController = null;

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
