using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;

namespace HMF.Enemy.EnemyStates
{
    public class EnemyMove : IState
    {
        private Enemy _enemy;
        private Rigidbody2D _rigidbody;

        public EnemyMove(Enemy enemy, Rigidbody2D rigidbody)
        {
            _enemy = enemy;
            _rigidbody = rigidbody;
        }

        public void OnEnter()
        {

        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            var dir = 1;

            if(_enemy.transform.position.x < _enemy.Target.transform.position.x)
            {
                dir = -1;
            }

            Transform tar = null;

            if (dir == 1)
            {
                tar = _enemy.left;
            }
            else if(dir == -1)
            {
                tar = _enemy.right;
            }

            if(Vector2.Distance(_enemy.transform.position, _enemy.Target.transform.position) > _enemy.attackRange - 0.5f)
            {
                //_enemy.transform.transform.LookAt(_enemy.Target.transform);

                

                _rigidbody.velocity = Vector2.left * _enemy.speed * dir;
            }
            //Debug.Log("Move");
        }
    }
}
