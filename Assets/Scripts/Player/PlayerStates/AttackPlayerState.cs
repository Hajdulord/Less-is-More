using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;

namespace HMF.Player.PlayerStates
{
    public class AttackPlayerState : IState
    {
        public void OnEnter()
        {
            //Start Attack Animation
        }

        public void OnExit()
        {
            //Start Attack Animation
        }

        public void Tick()
        {
            Attack();
        }

        private void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
