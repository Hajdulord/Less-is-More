using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine.InputSystem;
using HMF.Player.PlayerStates;
using System;
using UnityEngine.UI;

namespace HMF.Player
{
    public class PlayerController : MonoBehaviour
    {   
        [Header("Editor references")]
        [SerializeField] private Animator _animator = null;
        [SerializeField] private Rigidbody2D _rigidbody2D = null;
        [SerializeField] private LayerMask _jumpLayerMask;
        [SerializeField] private Transform _spriteTransform = null;
        [SerializeField] private List<Image> _hearts = null;
        [SerializeField] public Transform attackPoint;
        [SerializeField] public LayerMask enemyLayers;

        [Header("Player fields")]
        [SerializeField] public int health = 5;
        [SerializeField] public int attackDamage = 10;
        [SerializeField] public float attackRate = 2f;
        [SerializeField] public float attackRange = 2.5f;
        [SerializeField] public float movementSpeed = 5f;
        [SerializeField] public float jumpForce = 10f;
        [SerializeField] public float fallMultiplier = 2.5f;
        [SerializeField] public float attackedPushBackForce = 5f;
        [SerializeField] public float pushBackTime = 2f;

        private float _nextAttackTime = 0f;
        private int _heartsIndex = 4;
        private StateMachine _stateMachine;

        private float distToGround;

        private int dir = 1;

        private float _moveVal = 0;
        public float MoveVal
        {
            get { return _moveVal; }
            private set
            {
                _moveVal = value;

                if(_moveVal > 0)
                {
                    _spriteTransform.rotation = Quaternion.identity;
                    dir = 1;
                }
                else if(_moveVal < 0)
                {
                    _spriteTransform.rotation = new Quaternion(0,-1,0,0);
                    dir = -1;
                }
            }
        }
        public bool Jumped {get; set;} = false;
        public bool Attacked {get; set;} = false;

        public bool DamageTaken {get; set;} = false;

        public void Awake() 
        {
            distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
            //Debug.Log("Awake start");
            _stateMachine = new StateMachine();

            var idle = new IdlePlayerState(this, _rigidbody2D);
            var move = new MovePlayerState(this, _rigidbody2D, _animator);
            var attack = new AttackPlayerState(this, _animator);
            var jump = new JumpPlayerState(this, _rigidbody2D, _animator);
            var airborne = new AirbornePlayerState(this, _rigidbody2D, _animator);

            At(idle, move, isMoving());
            At(move, idle, isIdle());

            At(idle, jump, isJumping());
            At(move, jump, isJumping());

            At(jump, airborne, isJumping());

            At(airborne, idle, isGrounded());
            At(airborne, move, isGrounded());

            At(idle, attack, isAttacking());
            At(jump, attack, isAttacking());
            At(airborne, attack, isAttacking());
            At(move, attack, isAttacking());

            At(attack, idle, isIdle());
            At(attack, move, isMoving());
            At(attack, airborne, isAirborne());

            Func<bool> isIdle() => () => MoveVal == 0 && Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.05f, _jumpLayerMask);
            Func<bool> isMoving() => () => MoveVal != 0 && !Jumped && Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.05f, _jumpLayerMask);
            Func<bool> isJumping() => () => Jumped;
            Func<bool> isGrounded() => () => Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.05f, _jumpLayerMask);
            Func<bool> isAirborne() => () => !Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.05f, _jumpLayerMask);
            Func<bool> isAttacking() => () => Attacked;
            
            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            //Debug.Log("Awake end");

            _stateMachine.SetState(idle);
        }

        public void MoveInput(InputAction.CallbackContext context)
        {
            //if (!context.performed) return;
            if(context.started)
            {
                MoveVal = context.ReadValue<float>();
            }
            else if(context.canceled)
            {
                MoveVal = 0f;
            }
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

        public void AttackInput(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                Attacked = false;
               return; 
            } 

            if (Time.time >= _nextAttackTime)
            {
               Attacked = true;
               _nextAttackTime = Time.time + 1f / attackRange;
            }
            
            //Debug.Log("A");
        }

        public void TakeDamage()
        {
            DamageTaken = true;

            health = Mathf.Max(0, --health);

            var color = _hearts[_heartsIndex].color;

            color.a = 0.5f;

            _hearts[_heartsIndex].color = color;

            _heartsIndex = Mathf.Max(0, --_heartsIndex);
            
            Debug.Log($"health: {health}, velocity: {_rigidbody2D.velocity}");
        }

        public void PushBack()
        {
            var force = attackedPushBackForce * -1 * dir;

            _rigidbody2D.velocity += Vector2.right * force;
        }

        public void Update() 
        { 
            _stateMachine?.Tick();
        }

    }
}