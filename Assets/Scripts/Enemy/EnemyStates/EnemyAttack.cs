using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using HMF.Player;

namespace HMF.Enemy.EnemyStates
{
    public class EnemyAttack : IState
    {
        private Enemy _enemy;

        public EnemyAttack(Enemy enemy)
        {
            _enemy = enemy;
        }

        private float _nextAttackTime = 0;
        public void OnEnter()
        {
            //throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            //throw new System.NotImplementedException();
            //_enemy.Target = null;
        }

        public void Tick()
        {
            //throw new System.NotImplementedException();
            //Debug.Log("Attack");


            if (Time.time >= _nextAttackTime)
            {
                _enemy.Target.GetComponent<PlayerController>().TakeDamage();
                _nextAttackTime = Time.time + 1f / _enemy.attackRate;
                //Debug.Log("Attack2");
            }
        }
    }
}
