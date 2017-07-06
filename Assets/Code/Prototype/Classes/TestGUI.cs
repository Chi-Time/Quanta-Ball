using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Prototype.Classes
{
    public class TestGUI : MonoBehaviour
    {
        private void OnGUI ()
        {
            GUI.Label (new Rect (150, 150, 250, 100), "Score: " + GameController.Instance.Score);

            if (GameController.CurrentState == GameStates.GameOver)
            {
                if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 25, 150, 50), "Restart"))
                    ResetLevel ();

                Cursor.visible = true;
            }
            else
                Cursor.visible = false;
        }

        private void ResetLevel ()
        {
        }
    }
}
