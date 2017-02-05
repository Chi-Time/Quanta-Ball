using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
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