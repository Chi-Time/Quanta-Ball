using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    public abstract class Piece : MonoBehaviour
    {
        public Vector3 SpawnPosition = Vector3.zero;
        public Vector3 LeavePosition = Vector3.zero;
        public Pool Pool = null;

        [SerializeField] protected float _SpawnTransitionSpeed = .5f;
        [SerializeField] protected float _LeaveTransitionSpeed = 1f;

        protected Transform _Transform = null;

        private void Awake ()
        {
            AssignValues ();
        }

        protected virtual void AssignValues ()
        {
            _Transform = GetComponent<Transform> ();
        }

        public void Move ()
        {
            if (this.isActiveAndEnabled)
                StartCoroutine (MoveToPosition (false, _SpawnTransitionSpeed));
        }

        public void Leave ()
        {
            if(this.isActiveAndEnabled)
                StartCoroutine (MoveToPosition (transform, _LeaveTransitionSpeed));
        }

        protected virtual IEnumerator MoveToPosition (bool isLeaving, float speed)
        {
            var i = 0.0f;
            var rate = 1.0f / speed;

            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;

                if (isLeaving)
                    _Transform.position = Vector3.Lerp (_Transform.position, LeavePosition, i);
                else
                    _Transform.position = Vector3.Lerp (_Transform.position, SpawnPosition, i);

                yield return new WaitForEndOfFrame ();
            }

            _Transform.position = new Vector3 (
                Mathf.RoundToInt (_Transform.position.x),
                Mathf.RoundToInt (_Transform.position.y),
                Mathf.RoundToInt (_Transform.position.z)
                );

            if (isLeaving)
                Cull ();
        }

        public abstract void Cull ();
    }
}
