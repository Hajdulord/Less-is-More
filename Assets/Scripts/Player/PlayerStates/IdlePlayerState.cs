using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class IdlePlayerState : IState
    {
        public void OnEnter()
        {
            //throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            //throw new System.NotImplementedException();
        }

        public void Tick()
        {
            //Debug.Log("Idle");
        }
    }
}
