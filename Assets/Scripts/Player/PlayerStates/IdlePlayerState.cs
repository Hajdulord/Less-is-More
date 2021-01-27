using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class IdlePlayerState : IState
    {
        private Rigidbody2D _rigidbody2D;
        public IdlePlayerState(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void OnEnter()
        {
            //throw new System.NotImplementedException();
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void OnExit()
        {
            //throw new System.NotImplementedException();
        }

        public void Tick()
        {
            // Debug.Log("Idle");
        }
    }
}
