using UnityEngine;
using System.Collections;
using System;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Pieces/Barrier", 0)]
    public class Barrier : Piece
    {
        //TODO: Find a better way to reset the shrink amount than this hack.
        /// <summary>
        /// The number of shrinks the object can do before it's culled.
        /// </summary>
        private int _ShrinkAmount = 4;
        /// <summary>
        /// Cached amount value so that amount can be restored upon object reset.
        /// </summary>
        private int _CachedShrinkAmount = 0;
        /// <summary>
        /// The scale to shrink the object by every time.
        /// </summary>
        private float _ShrinkScale = 0f;

        protected override void AssignValues ()
        {
            base.AssignValues ();

            _ShrinkScale = 1 / (float)_ShrinkAmount;
            _CachedShrinkAmount = _ShrinkAmount;
            this.gameObject.tag = "Barrier";
        }

        private void OnEnable ()
        {
            _ShrinkAmount = _CachedShrinkAmount;

            Move ();
        }

        private void OnCollisionEnter (Collision collision)
        {
            if (collision.gameObject.CompareTag ("Player"))
                Shrink ();
        }

        private void Shrink ()
        {
            _Transform.localScale = new Vector3 (
                _Transform.localScale.x - _ShrinkScale,
                _Transform.localScale.y - _ShrinkScale,
                _Transform.localScale.z - _ShrinkScale
                );

            _ShrinkAmount--;

            if (_ShrinkAmount <= 0)
                Cull ();
        }

        public override void Cull ()
        {
            if (Pool != null)
                Pool.ReturnBarrier (this);
        }
    }
}