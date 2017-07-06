using UnityEngine;

namespace Assets.Code.Prototype.Classes
{
    public class CameraController2D : MonoBehaviour
    {
        public float DampTime = 0.15f;
        public Transform Target = null;
        public bool FindPlayer = true;

        private Vector3 _Velocity = Vector3.zero;

        private void Start ()
        {
            if(FindPlayer)
                Target = GameObject.FindGameObjectWithTag ("Player").transform;
        }

        void FixedUpdate ()
        {
            SetTarget ();
            MoveCamera ();
        }

        private void SetTarget ()
        {
            if(GameController.Instance.IsGameOver)
                Target = null;
            else if (GameController.Instance.IsGameOver != null && Target == null)
                Target = GameObject.FindGameObjectWithTag ("Player").transform;
        }

        private void MoveCamera ()
        {
            if (Target)
            {
                var point = Camera.main.WorldToViewportPoint (Target.position);

                var delta = Target.position - Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z));
                var destination = transform.position + delta;

                transform.position = Vector3.SmoothDamp (transform.position, destination, ref _Velocity, DampTime);
            }
        }
    }
}
