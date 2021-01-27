using HMF.HMFUtilities.DesignPatterns.StatePattern;
using HMF.Player;
using UnityEngine;

namespace HMF
{
    public class AirbornePlayerState : IState
    {
        private PlayerController _playerControler;
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity;
        public AirbornePlayerState(PlayerController player, Rigidbody2D rigidbody2D)
        {
            _playerControler = player;
            _rigidbody2D = rigidbody2D;
            _velocity = Vector2.zero;
        }

        public void OnEnter()
        { 
            //_velocity = _rigidbody2D.velocity;
        }

        public void OnExit()
        {
            _playerControler.Jumped = false;
            _velocity = Vector2.zero;
        }

        public void Tick()
        {
            // Debug.Log("Falling");

            if(_rigidbody2D.velocity.y < 0)
            {
                _velocity += Vector2.up * Physics2D.gravity * (_playerControler.fallMultiplier - 1) * Time.deltaTime;
                _velocity.x = _playerControler.MoveVal * _playerControler.movementSpeed;
                _rigidbody2D.velocity = _velocity;
            }else if (_rigidbody2D.velocity.y > 0)
            {
                _velocity = _rigidbody2D.velocity;
                _velocity.x = _playerControler.MoveVal * _playerControler.movementSpeed;
                _rigidbody2D.velocity = _velocity;
            }
            
            //Debug.Log(_velocity);
        }
    }
}
