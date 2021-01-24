using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class JumpPlayerState : IState
    {
        private PlayerController _player;
        private Rigidbody2D _rigidbody2D;

        public JumpPlayerState(PlayerController player, Rigidbody2D rigidbody2D)
        {
            _player = player;
            _rigidbody2D = rigidbody2D;
        }

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
            _rigidbody2D.velocity += Vector2.up * _player.JumpForce; 
            Debug.Log("Jump");
        }
    }
}
