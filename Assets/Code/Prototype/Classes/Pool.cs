using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Prototype.Classes
{
    public class Pool : MonoBehaviour
    {
        [Header("Configuration")]
        [Tooltip("The number of floors pre-spawned into the floors pool.")]
        private int _FloorPoolSize = 35;
        [Tooltip("The number barriers pre-spawned into the barriers pool.")]
        private int _BarrierPoolSize = 12;

        [Space(20)]
        [Header("Objects")]
        [Tooltip("The floor prefab to use.")]
        private Piece _FloorPrefab = null;
        [Tooltip("The barrier prefab to use.")]
        private Piece _BarrierPrefab = null;

        [Space (20)]
        [Header("Pools")]
        [Tooltip("The object pool of Floors. NOTE: Read only, don't edit.")]
        private List<Piece> _Floors = new List<Piece> ();

        [Tooltip ("The object pool of Barriers. NOTE: Read only, don't edit.")]
        private List<Piece> _Barriers = new List<Piece> ();

        private MonoBehaviour _Behaviour = null;

        public void Init (MonoBehaviour behaviour)
        {
            _Behaviour = behaviour;
            GeneratePools ();
        }

        private void GeneratePools ()
        {

        }

        private Piece SpawnFloor ()
        {
            return null;
        }

        private Piece SpawnBarrier ()
        {
            return null;
        }

        private GameObject SetupObject (GameObject go, string parent, string name)
        {
            return null;
        }

        public Piece RetrieveFloor ()
        {
            return null;
        }

        public Piece RetrieveBarrier ()
        {
            return null;
        }

        public void ReturnFloor (Piece floor)
        {

        }

        public void ReturnBarrier (Piece barrier)
        {

        }

        private Piece ResetPiece (Piece piece)
        {
            return null;
        }
    }
}
