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
        }

        public void OnExit() 
        {
            _collider = null;
            _player.Attacked = false;
        }

        public void Tick()
        {
            if (_collider == null) return;

            Debug.Log(_collider.name);
        }
    }
}
