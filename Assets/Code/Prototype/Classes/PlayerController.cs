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
        private float _CMovementSpeed = 0.0f;
        private Vector3 _MovementDirection = Vector3.zero;
        private GameController _GC = null;

        public void ResetBall ()
        {
            _Rigidbody.velocity = Vector3.zero;
            _MovementSpeed = _CMovementSpeed;
            _MovementDirection = Vector3.zero;
            _Transform.position = new Vector3 (6f, 0f, 0f);
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
            _GC = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
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
            _CMovementSpeed = _MovementSpeed;
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
                EventManager.ChangeState (GameStates.Game);
                _Rigidbody.velocity = _MovementDirection * _MovementSpeed * Time.fixedDeltaTime;
            }
            else
            {
                EventManager.ChangeState (GameStates.GameOver);
                _Rigidbody.velocity = new Vector3 (_Rigidbody.velocity.x, _Rigidbody.velocity.y, _Rigidbody.velocity.z);
            }
        }

        private bool IsGrounded ()
        {
            var endPos = new Vector3 (_Transform.position.x, _Transform.position.y - 1f, _Transform.position.z);

            if (Physics.Linecast (_Transform.position, endPos, _GroundLayer))
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
            if(other.gameObject.CompareTag ("Sequence Trigger"))
            {
                //TODO: Implement generator callback for next sequence.
                _GC.GetComponent<LevelGenerator> ().StartSequence ();
                _GC.Score++;

                Destroy (other.gameObject);
            }
        }
    }
}
