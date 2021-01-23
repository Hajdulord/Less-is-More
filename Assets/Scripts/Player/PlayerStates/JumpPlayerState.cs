using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;

namespace HMF.Player.PlayerStates
{
    public class JumpPlayerState : IState
    {
        public void OnEnter()
        {
            // Start Jump Animation
        }

        public void OnExit()
        {
            // Stop Jump Animation
        }

        public void Tick()
        {
            Jump();
        }

        private void Jump()
        {
            throw new NotImplementedException();
        }
    }
}
