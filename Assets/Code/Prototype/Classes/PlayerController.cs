using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Controllers/Player Controller", 0)]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// The speed of the player's movement in a given direction.
        /// </summary>
        [Tooltip ("The speed of the player's movement in a given direction.")]
        public float _MovementSpeed = 350.0f;

        [SerializeField] private LayerMask _GroundLayer;

        private Rigidbody _Rigidbody = null;
        private Transform _Transform = null;
        private Vector3 _MovementDirection = Vector3.zero;

        public void ResetBall ()
        {
            _Rigidbody.velocity = Vector3.zero;
            //TODO: Cache value later.
            _MovementSpeed = 350.0f;
            _MovementDirection = Vector3.zero;
        } 

        private void Awake ()
        {
            AssignReferences ();
        }

        private void AssignReferences ()
        {
            _Rigidbody = GetComponent<Rigidbody> ();
            _Transform = GetComponent<Transform> ();
            this.gameObject.layer = LayerMask.NameToLayer ("Water");
        }

        private void Start ()
        {
            SetDefaults ();
        }

        private void SetDefaults ()
        {
            _MovementDirection = Vector3.zero;
            _Rigidbody.useGravity = true;
            _Rigidbody.isKinematic = false;
        }

        private void Update ()
        {
            GetInput ();
        }

        private void GetInput ()
        {
            #if UNITY_STANDALONE
            if (Input.GetButtonDown("Fire1"))
                ChangeMovementDirection ();
            #endif
            #if UNITY_ANDROID
            if (Input.GetMouseButtonDown (0))
                ChangeMovementDirection ();
            #endif
        }

        private void ChangeMovementDirection ()
        {
            // Right
            if (_MovementDirection == Vector3.forward || _MovementDirection == Vector3.back)
                _MovementDirection = Vector3.left;
            // Forward
            else if (_MovementDirection == Vector3.right || _MovementDirection == Vector3.left)
                _MovementDirection = Vector3.forward;
            // Player is still, move right to begin with.
            else
                _MovementDirection = Vector3.left;
        }

        private void FixedUpdate ()
        {
            Move ();
        }

        private void Move ()
        {
            if(IsGrounded())
            {
                GameController.Instance.IsGameOver = false;
                _Rigidbody.velocity = _MovementDirection * _MovementSpeed * Time.fixedDeltaTime;
            }
            else
            {
                GameController.Instance.IsGameOver = true;
                _Rigidbody.velocity = new Vector3 (_Rigidbody.velocity.x, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
            }
        }

        private bool IsGrounded ()
        {
            if (Physics.Linecast (_Transform.position, new Vector3 (_Transform.position.x, _Transform.position.y - 1f, _Transform.position.z), _GroundLayer))
                return true;

            return false;
        }

        private void OnCollisionEnter (Collision collision)
        {
            if (collision.gameObject.CompareTag ("Barrier"))
                InverseDirection ();
        }

        private void InverseDirection ()
        {
            // Move back.
            if (_MovementDirection == Vector3.forward)
                _MovementDirection = Vector3.back;
            // Move forward.
            else if (_MovementDirection == Vector3.back)
                _MovementDirection = Vector3.forward;
            // Move left.
            else if (_MovementDirection == Vector3.left)
                _MovementDirection = Vector3.right;
            // Move right.
            else if (_MovementDirection == Vector3.right)
                _MovementDirection = Vector3.left;
        }

        private void OnTriggerEnter (Collider other)
        {
            if(other.gameObject.CompareTag("Sequence Trigger"))
            {
                //TODO: Implement generator callback for next sequence.
                var lg = GameController.Instance.GetComponent<LevelGenerator> ();
                lg.StartSequence ();
                GameController.Instance.Score++;
                Destroy (other.gameObject);
            }
        }
    }
}
