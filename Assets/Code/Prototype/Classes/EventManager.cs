using UnityEngine;
using System.Collections;

namespace Assets.Code.Prototype.Classes
{
    public delegate void SwitchState (GameStates state);

    public static class EventManager
    {
        public static event SwitchState OnStateSwitched;

        public static void ChangeState (GameStates state)
        {
            OnStateSwitched (state);
        }
    }
}
