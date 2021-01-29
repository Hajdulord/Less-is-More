using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;

namespace HMF.Enemy.EnemyStates
{
    public class EnemyIdle : IState
    {
        private Enemy _enemy;

        public EnemyIdle(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            var collider = Physics2D.OverlapCircle(_enemy.gameObject.transform.position, _enemy.searchRange, _enemy.playerLayer);
            if (collider != null)
            {
                _enemy.Target = collider.gameObject;
            }

            //Debug.Log($"Idle :{collider}");
        }
    }
}
