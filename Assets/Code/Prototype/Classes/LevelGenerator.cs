using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Controllers/Level Generator")]
    public class LevelGenerator : MonoBehaviour
    {
        [Tooltip("The object pool this generator relies on.")]
        [SerializeField] private Pool _Pool = null;
        [Tooltip("The current objects ready to be culled. NOTE: Read only, don't edit.")]
        [SerializeField] private List<Piece> _CullingPool = new List<Piece> ();
        [Tooltip("The current flooring objects which have just been created. NOTE: Read only, don't edit.")]
        [SerializeField] private List<Piece> _CurrentSequence = new List<Piece> ();

        private Vector3 _Anchor = Vector3.zero;
        private bool _IsForward = false;

        private void Awake ()
        {
        }

        private void AssignReferences ()
        {
        }

        private void SpawnIntitialFlooring ()
        {
        }

        private Piece GetFloor (Vector3 pos)
        {
            return null;
        }

        private Piece GetBarrier (Vector3 pos)
        {
            return null;
        }

        private GameObject SpawnSequenceTrigger (Vector3 pos)
        {
            return null;
        }

        private void Start ()
        {
        }
        
        public void StartSequence ()
        {
        }

        private void SpawnBarriers ()
        {
        }

        private IEnumerator SpawnFlooringSequence (float delay)
        {
            yield return new WaitForSeconds (delay);
        }

        private void CullObjects ()
        {
        }

        private void SwitchSystems ()
        {
        }
    }
}