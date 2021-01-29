using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;

namespace HMF.Player.PlayerStates
{
    public class MovePlayerState : IState
    {
        private PlayerController _player;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private Vector2 _velocity;
        private float _nextAttackTime = 0f;

        public MovePlayerState(PlayerController player, Rigidbody2D rigidbody2D, Animator animator)
        {
            _player = player;
            _rigidbody2D = rigidbody2D;
            _velocity = Vector2.zero;
            _animator = animator;
        }

        public void OnEnter()
        {
            // Start Move Animation
            _animator.SetBool("isRunning", true);
            _nextAttackTime = _player.pushBackTime;
        }

        public void OnExit()
        {
            // Stop Move Animation
            _animator.SetBool("isRunning", false);
        }

        public void Tick()
        {
            /*_velocity.x = _player.MoveVal * _player.movementSpeed;
            _velocity.y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity  = _velocity;*/
            //_rigidbody2D.AddForce(_velocity);
            
            _rigidbody2D.velocity = Vector2.right * _player.MoveVal * _player.movementSpeed;
            //Debug.Log(_rigidbody2D.velocity);
            //Debug.Log("Move");

            if (_player.DamageTaken)
            {
                _player.PushBack();
            }

            if (Time.time >= _nextAttackTime)
            {
               _player.DamageTaken = false;
               _nextAttackTime = Time.time + _player.pushBackTime;
            }
        }
    }
}
