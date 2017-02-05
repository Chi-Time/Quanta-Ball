using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    [AddComponentMenu("Pieces/Floor", 1)]
    public class Floor : Piece
    {
        private void OnEnable ()
        {
            Move ();
        }

        public override void Cull ()
        {
            //TODO: Return item to pool.
        }
    }
}