using System;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine;
namespace HMF.Player.PlayerStates
{
    public class AttackPlayerState : IState
    {
        private PlayerController _player;
        private Collider2D _collider;
        private Animator _animator;
        
        private HMF.Enemy.Enemy _enemy = null;

        public AttackPlayerState(PlayerController player, Animator animator)
        {
            _animator = animator;
            _player = player;
        }

        public void OnEnter()
        {
            //Start Attack Animation
            _animator.SetTrigger("Attacking");
            _collider = Physics2D.OverlapCircle(_player.attackPoint.position, _player.attackRange, _player.enemyLayers);

            if(_collider != null)
                _enemy = _collider.GetComponent<HMF.Enemy.Enemy>();
        }

        public void OnExit() 
        {
            _collider = null;
            _player.Attacked = false;
        }

        public void Tick()
        {
            if (_collider == null && _enemy == null) return;

            _enemy.TakeDamage(_player.attackDamage);
            Debug.Log(_enemy.health);

            if (_player.DamageTaken)
            {
                _player.PushBack();
            }
        }
    }
}
