using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HMF.Player;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using HMF.Enemy.EnemyStates;

namespace HMF.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public int health = 50;
        [SerializeField] public float speed = 5f;
        [SerializeField] public float attackRate = 5f;
        [SerializeField] public float attackRange = 5f;
        [SerializeField] public float searchRange = 50;
        [SerializeField] public float deathCounter = 5f;


        [SerializeField] public LayerMask playerLayer;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] public Transform left;
        [SerializeField] public Transform right;
        [SerializeField] private Collider2D _collider;

        public GameObject Target {get; set;} = null;

        private StateMachine _stateMachine;
        private void Awake() {
            
            _stateMachine = new StateMachine();

            var idle = new EnemyIdle(this);
            var attack = new EnemyAttack(this);
            var move = new EnemyMove(this, _rigidbody);
            var death = new EnemyDeath(this, _collider);

            At(idle, move, hasTarget());
            At(move, idle, lostTarget());

            At(move, attack, reachedTarget());

            At(attack, idle, lostTarget());
            At(attack, move, targetOutOfRange());

            At(idle, death, isDead());
            At(move, death, isDead());
            At(attack, death, isDead());

            Func<bool> hasTarget() => () => Target != null;
            Func<bool> lostTarget() => () => Target == null;
            Func<bool> reachedTarget() => () => Vector2.Distance(transform.position, Target.transform.position) < attackRange - 0.5f;
            Func<bool> targetOutOfRange() => () => Target != null && Vector2.Distance(transform.position, Target.transform.position) > attackRange - 0.5f;
            Func<bool> isDead() => () => health <= 0;

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            _stateMachine.SetState(idle);
        }

        public void TakeDamage(int damage)
        {
            health = Mathf.Max(0, health - damage);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();

            player?.TakeDamageFromCollision();
        }

        private void Update()
        {
            _stateMachine?.Tick();
        }

    }

    
}
