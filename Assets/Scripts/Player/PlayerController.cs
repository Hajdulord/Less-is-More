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
        //[Header("Editor references")]
        //[SerializeField] private Animator _animator = null;
        //[SerializeField] private PlayerInput _playerInput = null;
        [SerializeField] private Rigidbody2D _rigidbody2D = null;

        [Header("Player fields")]
        [SerializeField] public float movementSpeed = 5f;

        private StateMachine _stateMachine;

        public float MoveVal{get; private set;} = 0;

        public void Awake() 
        {
            //Debug.Log("Awake start");
            _stateMachine = new StateMachine();

            var idle = new IdlePlayerState();
            var move = new MovePlayerState(this, _rigidbody2D);
            var attack = new AttackPlayerState();
            var jump = new JumpPlayerState();

            _stateMachine.AddAnyTransition(idle, isIdle());
            _stateMachine.AddAnyTransition(move, moving());
            At(idle, move, moving());
            At(move, idle, isIdle());


            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> isIdle() => () => MoveVal == 0;
            Func<bool> moving() => () => MoveVal != 0;

            //Debug.Log("Awake end");
        }

        public void MoveInput(InputAction.CallbackContext context)
        {
            MoveVal = context.ReadValue<float>();
            //Debug.Log($"MoveVal: {MoveVal}");
        }

        public void JumpInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Debug.Log("Jump");
        }

        public void Update() => _stateMachine?.Tick();

    }
}