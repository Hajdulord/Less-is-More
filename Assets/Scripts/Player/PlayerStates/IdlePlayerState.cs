using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class IdlePlayerState : IState
    {
        private Rigidbody2D _rigidbody2D;
        private PlayerController _player;
        public IdlePlayerState(PlayerController player, Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
            _player = player;
        }

        public void OnEnter()
        {
            //throw new System.NotImplementedException();
            _rigidbody2D.velocity = Vector2.zero;
            _player.DamageTaken = false;
        }

        public void OnExit()
        {
            //throw new System.NotImplementedException();
        }

        public void Tick()
        {
            //Debug.Log("Idle");
            if (_player.DamageTaken)
            {
                _player.PushBack();
            }
        }
    }
}
