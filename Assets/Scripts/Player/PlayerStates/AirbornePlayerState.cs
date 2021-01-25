using HMF.HMFUtilities.DesignPatterns.StatePattern;
using HMF.Player;
using UnityEngine;

namespace HMF
{
    public class AirbornePlayerState : IState
    {
        private PlayerController _playerControler;
        private Rigidbody2D _rigidbody2D;

        public AirbornePlayerState(PlayerController player, Rigidbody2D rigidbody2D)
        {
            _playerControler = player;
            _rigidbody2D = rigidbody2D;
        }

        public void OnEnter(){ }

        public void OnExit()
        {
            _playerControler.Jumped = false;
        }

        public void Tick()
        {
            //Debug.Log("Falling");
            Vector2 velocity = Vector2.zero;
            velocity.x = _playerControler.MoveVal * _playerControler.movementSpeed;
            velocity.y = _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = velocity;
        }
    }
}
