using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class JumpPlayerState : IState
    {
        private PlayerController _player;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;

        public JumpPlayerState(PlayerController player, Rigidbody2D rigidbody2D, Animator animator)
        {
            _player = player;
            _rigidbody2D = rigidbody2D;
            _animator = animator;
        }

        public void OnEnter()
        {
            // Start Jump Animation
            _animator.SetBool("isJumping", true);
        }

        public void OnExit()
        {
            // Stop Jump Animation
            _animator.SetBool("isJumping", false);
        }

        public void Tick()
        {
            var appliedVelocity = Vector2.up * _player.jumpForce;
            appliedVelocity.x = _player.MoveVal * _player.movementSpeed;

            _rigidbody2D.velocity = appliedVelocity;

            //Debug.Log(_rigidbody2D.velocity);
            //Debug.Log("Jump");
        }
    }
}
