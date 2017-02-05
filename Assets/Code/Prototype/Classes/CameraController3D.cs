using UnityEngine;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Controllers/3D Camera Controller")]
    public class CameraController3D : MonoBehaviour
    {
        /// <summary>
        /// The target object to follow.
        /// </summary>
        [Tooltip ("The target object to follow.")]
        public Transform Target;
        /// <summary>
        /// The distance to keep from the target.
        /// </summary>
        [Tooltip ("The distance to keep from the target.")]
        public float Distance = 3.0f;
        /// <summary>
        /// How high we are from the target.
        /// </summary>
        [Tooltip ("How high we are from the target.")]
        public float Height = 3.0f;
        /// <summary>
        /// How fast we rotate to keep focus.
        /// </summary>
        [Tooltip ("How fast we rotate to keep focus.")]
        public float Damping = 5.0f;
        /// <summary>
        /// Are we using smooth rotation?
        /// </summary>
        [Tooltip ("Are we using smooth rotation?")]
        public bool SmoothRotation = true;
        /// <summary>
        /// Should we follow behind the target.
        /// </summary>
        [Tooltip ("Should we follow behind the target.")]
        public bool FollowBehind = true;
        /// <summary>
        /// How fast we rotate.
        /// </summary>
        [Tooltip ("How fast we rotate.")]
        public float RotationDamping = 10.0f;
        [Tooltip ("Should the component look for a player object?")]
        public bool FindPlayer = true;

        void Start ()
        {
            if (FindPlayer)
                // Assign target reference.
                Target = GameObject.FindGameObjectWithTag ("Player").transform;
        }

        void LateUpdate ()
        {
            // Is there a target to lock on to.
            if (Target != null)
            {
                Vector3 wantedPosition;

                if (FollowBehind)
                    wantedPosition = Target.TransformPoint (0, Height, -Distance);
                else
                    wantedPosition = Target.TransformPoint (0, Height, Distance);

                transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * Damping);

                if (SmoothRotation)
                {
                    Quaternion wantedRotation = Quaternion.LookRotation (Target.position - transform.position + new Vector3 (-2, 0, 1), Target.up);

                    transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * RotationDamping);
                }
                else
                {
                    transform.LookAt (Target, Target.up);
                }
            }
            // Is there no target?
            else
            {
                if (FindPlayer)
                {
                    // Keep looking for the target and re-assign it when found.
                    Target = GameObject.FindGameObjectWithTag ("Player").transform;
                }
            }
        }
    }
}
