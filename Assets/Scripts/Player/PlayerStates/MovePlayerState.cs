using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class MovePlayerState : IState
    {
        private PlayerController _player;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;

        public MovePlayerState(PlayerController player, Rigidbody2D rigidbody2D)
        {
            _player = player;
            _rigidbody2D = rigidbody2D;
            _velocity = Vector2.zero;
        }

        public void OnEnter()
        {
            // Start Move Animation
        }

        public void OnExit()
        {
            // Stop Move Animation
        }

        public void Tick()
        {
            /*_velocity.x = _player.MoveVal * _player.movementSpeed;
            _velocity.y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity  = _velocity;*/
            //_rigidbody2D.AddForce(_velocity);
            
            _rigidbody2D.velocity = Vector2.right * _player.MoveVal * _player.movementSpeed;
            //Debug.Log(_rigidbody2D.velocity);
        }
    }
}
