using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine.InputSystem;
using HMF.Player.PlayerStates;
using System;

namespace HMF.Player
{
    public class PlayerController : MonoBehaviour
    {   
        [Header("Editor references")]
        //[SerializeField] private Animator _animator = null;
        //[SerializeField] private PlayerInput _playerInput = null;
        [SerializeField] private Rigidbody2D _rigidbody2D = null;
        [SerializeField] private LayerMask _layerMask;

        [Header("Player fields")]
        [SerializeField] public float movementSpeed = 5f;
        [SerializeField] public float JumpForce = 10f;

        private StateMachine _stateMachine;

        private float distToGround;

        public float MoveVal{get; private set;} = 0;
        public bool Jumped{get; set;} = false;

        public void Awake() 
        {
            distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
            //Debug.Log("Awake start");
            _stateMachine = new StateMachine();

            var idle = new IdlePlayerState(_rigidbody2D);
            var move = new MovePlayerState(this, _rigidbody2D);
            var attack = new AttackPlayerState();
            var jump = new JumpPlayerState(this, _rigidbody2D);
            var airborne = new AirbornePlayerState(this);

            //_stateMachine.AddAnyTransition(idle, isIdle());
            //_stateMachine.AddAnyTransition(move, moving());
            At(idle, move, isMoving());
            At(move, idle, isIdle());
            At(move, move, isMoving());

            At(idle, jump, isJumping());
            At(move, jump, isJumping());

            At(jump, airborne, isJumping());
            At(airborne, airborne, isAirborne());

            At(airborne, idle, isGrounded());
            At(airborne, move, isGrounded());


            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> isIdle() => () => MoveVal == 0;
            Func<bool> isMoving() => () => MoveVal != 0 && !Jumped;
            Func<bool> isJumping() => () => Jumped;
            Func<bool> isAirborne() => () => !Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.05f, _layerMask);
            Func<bool> isGrounded() => () => Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.05f, _layerMask);
            

            //Debug.Log("Awake end");

            _stateMachine.SetState(idle);
        }

        public void MoveInput(InputAction.CallbackContext context)
        {
            MoveVal = context.ReadValue<float>();
            //Debug.Log($"MoveVal: {MoveVal}");
        }

        public void JumpInput(InputAction.CallbackContext context)
        {
            if (!context.performed && Jumped) return;

            //Debug.Log($"Jump first: {Jumped}");

            Jumped = true;

            //Debug.Log($"Jump second: {Jumped}");

            //Debug.Log("Jump");
        }

        public void Update() 
        { 
            _stateMachine?.Tick();
        }

    }
}