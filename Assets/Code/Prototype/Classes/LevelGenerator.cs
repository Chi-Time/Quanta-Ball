using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Controllers/Level Generator")]
    public class LevelGenerator : MonoBehaviour
    {
        [Tooltip ("The object pool this generator relies on.")]
        [SerializeField] private Pool _Pool = new Pool ();
        [Tooltip("The current objects ready to be culled. NOTE: Read only, don't edit.")]
        [SerializeField] private List<Piece> _CullingPool = new List<Piece> ();
        [Tooltip("The current flooring objects which have just been created. NOTE: Read only, don't edit.")]
        [SerializeField] private List<Piece> _CurrentSequence = new List<Piece> ();

        private Vector3 _Anchor = Vector3.zero;
        private bool _IsForward = false;

        private void Awake ()
        {
            AssignReferences ();
            SpawnIntitialFlooring ();
        }

        private void AssignReferences ()
        {
            _Pool.Init (this);
        }

        private void SpawnIntitialFlooring ()
        {
            for (int i = 0; i < 7; i++)
                _CurrentSequence.Add (GetFloor (new Vector3 (i, -2f, 0f)));
        }

        //TODO: Consider making abstract method that uses an enum to return a floor or barrier.
        private Piece GetFloor (Vector3 pos)
        {
            var floor = _Pool.RetrieveFloor ();
            // Place the object below it's spawn position so that it can move up.
            floor.transform.position = new Vector3 (pos.x, pos.y - 15f, pos.z);
            floor.transform.rotation = Quaternion.Euler (new Vector3 (-90f, 0f, 0f));
            floor.gameObject.SetActive (true);

            // Set the spawn position as the desired final place of the floor.
            floor.SpawnPosition = pos;
            floor.LeavePosition = new Vector3 (pos.x, pos.y - 15f, pos.z);

            return floor;
        }

        private Piece GetBarrier (Vector3 pos, Vector3 angle)
        {
            var barrier = _Pool.RetrieveBarrier ();
            barrier.transform.position = new Vector3 (pos.x, pos.y + 15f, pos.z);
            barrier.transform.rotation = Quaternion.Euler (angle);
            barrier.gameObject.SetActive (true);

            barrier.SpawnPosition = pos;
            barrier.LeavePosition = new Vector3 (pos.x, pos.y - 15f, pos.z);

            return barrier;
        }

        private GameObject SpawnSequenceTrigger (Vector3 pos)
        {
            var go = new GameObject ("Sequence Trigger");
            go.transform.position = pos;
            go.tag = "Sequence Trigger";

            var col = go.AddComponent<BoxCollider> ();
            col.size = new Vector3 (1, 1, 1);
            col.isTrigger = true;

            return go;
        }

        private void Start ()
        {
            StartCoroutine (SpawnFlooringSequence (.05f));
        }
        
        public void StartSequence ()
        {
            // Cull all previous objects from the last sequence.
            CullObjects ();
            // Spawn the barriers for the current sequence.
            SpawnBarriers ();
            // Spawn a new flooring sequence.
            StartCoroutine (SpawnFlooringSequence (.05f));
        }

        private void SpawnBarriers ()
        {
            //TODO: Consider placing if statement within for loop for refactoring purposes.
            if(_IsForward)
            {
                // Place anchors at the beginning and end of the flooring sequence.
                for (int i = 0; i < 7; i++)
                    if (i == 0 || i == 6)
                        _CullingPool.Add (GetBarrier (new Vector3 (_Anchor.x, 0f, _Anchor.z + (i + 1)), new Vector3 (90f, 0f, 90f)));
            }
            else
            {
                for (int i = 0; i < 7; i++)
                    if (i == 0 || i == 6)
                        _CullingPool.Add (GetBarrier (new Vector3 (_Anchor.x - (i + 1), 0f, _Anchor.z), new Vector3 (90f, 90f, 90f)));
            }
        }

        private IEnumerator SpawnFlooringSequence (float delay)
        {
            //TODO: Consider adding these calls to the StartSequence() method for clarity and SRS.
            // Re-assign the anchor position.
            ReassignAnchor ();
            // Mark all of the current objects to be culled.
            SwitchSystems ();

            if(_IsForward)
            {
                for(int i = 0; i < 7; i++)
                {
                    if (i == 4)
                        SpawnSequenceTrigger (new Vector3 (_Anchor.x - i, 0f, _Anchor.z));

                    _CurrentSequence.Add (GetFloor (new Vector3 (_Anchor.x - i, 0f, _Anchor.z)));

                    yield return new WaitForSeconds (delay);
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i == 4)
                        SpawnSequenceTrigger (new Vector3 (_Anchor.x, 0f, _Anchor.z + i));

                    _CurrentSequence.Add (GetFloor (new Vector3 (_Anchor.x, -2f, _Anchor.z + (i + 1))));

                    yield return new WaitForSeconds (delay);
                }
            }

            _IsForward = !_IsForward;
        }

        private void ReassignAnchor ()
        {
            _Anchor = _CurrentSequence[Random.Range (1, _CurrentSequence.Count - 1)].transform.position;
        }

        private void CullObjects ()
        {
            for (int i = 0; i < _CullingPool.Count; i++)
                _CullingPool[i].Leave ();

            _CullingPool.Clear ();
            _CullingPool.TrimExcess ();
        }

        //TODO: Consider better name to make method intentions more clear.
        private void SwitchSystems ()
        {
            for (int i = 0; i < _CurrentSequence.Count; i++)
                _CullingPool.Add (_CurrentSequence[i]);

            _CurrentSequence.Clear ();
            _CurrentSequence.TrimExcess ();
        }
    }
}