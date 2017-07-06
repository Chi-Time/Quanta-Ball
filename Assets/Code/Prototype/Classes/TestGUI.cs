﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Prototype.Classes
{
    public class TestGUI : MonoBehaviour
    {
        private void OnGUI ()
        {
            GUI.Label (new Rect (150, 150, 250, 100), "Score: " + GameController.Instance.Score);

            if (GameController.Instance.IsGameOver)
            {
                if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 25, 150, 50), "Restart"))
                {
                    //SceneManager.LoadScene (0);
                    ResetLevel ();
                }

                Cursor.visible = true;
            }
            else
                Cursor.visible = false;
        }

        private void ResetLevel ()
        {
            // Grab the level generator
            var lg = GameObject.FindGameObjectWithTag ("GameController").GetComponent<LevelGenerator> ();
            lg.ResetLevel ();

            // Grab the player
            var p = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
            p.ResetBall ();

            GameController.Instance.Score = 0;
        }
    }
}
