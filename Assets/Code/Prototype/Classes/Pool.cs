using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Prototype.Classes
{
    [System.Serializable]
    public class Pool
    {
        [Header("Configuration")]
        [Tooltip("The number of floors pre-spawned into the floors pool.")]
        [SerializeField] private int _FloorPoolSize = 35;
        [Tooltip("The number barriers pre-spawned into the barriers pool.")]
        [SerializeField] private int _BarrierPoolSize = 12;

        [Header("Objects")]
        [Tooltip("The floor prefab to use.")]
        [SerializeField] private Piece _FloorPrefab = null;
        [Tooltip("The barrier prefab to use.")]
        [SerializeField] private Piece _BarrierPrefab = null;

        [Header("Pools")]
        [Tooltip("The object pool of Floors. NOTE: Read only, don't edit.")]
        [SerializeField] private List<Piece> _Floors = new List<Piece> ();
        [Tooltip ("The object pool of Barriers. NOTE: Read only, don't edit.")]
        [SerializeField] private List<Piece> _Barriers = new List<Piece> ();

        public void Init (MonoBehaviour behaviour)
        {
            CreateObjectHolders (behaviour);
            GeneratePools ();
        }

        private void GeneratePools ()
        {
            for (int i = 0; i < _FloorPoolSize; i++)
                _Floors.Add (SpawnFloor ());

            for (int i = 0; i < _BarrierPoolSize; i++)
                _Barriers.Add (SpawnBarrier ());
        }

        private void CreateObjectHolders (MonoBehaviour behaviour)
        {
            var level = new GameObject ("Level");
            var floors = new GameObject ("Floors");
            var barriers = new GameObject ("Barriers");

            level.transform.SetParent (behaviour.transform);
            floors.transform.SetParent (level.transform);
            barriers.transform.SetParent (level.transform);
        }

        private Piece SpawnFloor ()
        {
            var go = (GameObject)Object.Instantiate (_FloorPrefab.gameObject, Vector3.zero, Quaternion.identity);

            var c = SetupObject (go, "Floors", "Floor").GetComponent<Piece> ();
            c.Pool = this;

            return c;
        }

        private Piece SpawnBarrier ()
        {
            var go = (GameObject)Object.Instantiate (_BarrierPrefab.gameObject, Vector3.zero, Quaternion.identity);

            var c = SetupObject (go, "Barriers", "Barrier").GetComponent<Piece> ();
            c.Pool = this;

            return c;
        }

        private GameObject SetupObject (GameObject go, string parent, string name)
        {
            go.transform.SetParent (GameObject.Find (parent).transform);
            go.SetActive (false);
            go.name = name;

            return go;
        }

        public Piece RetrieveFloor ()
        {
            if(_Floors.Count > 0)
            {
                var floor = _Floors[0];
                _Floors.Remove (floor);
                floor.gameObject.SetActive (true);
                return floor;
            }

            return null;
        }

        public Piece RetrieveBarrier ()
        {
            if (_Barriers.Count > 0)
            {
                var barrier = _Barriers[0];
                _Barriers.Remove (barrier);
                barrier.gameObject.SetActive (true);
                return barrier;
            }

            return null;
        }

        public void ReturnFloor (Piece floor)
        {
            _Floors.Add (ResetPiece (floor));
        }

        public void ReturnBarrier (Piece barrier)
        {
            _Barriers.Add (ResetPiece (barrier));
        }

        private Piece ResetPiece (Piece piece)
        {
            piece.transform.localScale = Vector3.one;
            piece.transform.position = Vector3.zero;
            piece.gameObject.SetActive (false);

            return piece;
        }
    }
}
