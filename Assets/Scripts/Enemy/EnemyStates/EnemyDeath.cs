using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;
using UnityEngine.SceneManagement;

namespace HMF.Enemy.EnemyStates
{
    public class EnemyDeath : IState
    {
        private Collider2D _collider;
        private Enemy _enemy;
        private float counter = 0;
        public EnemyDeath(Enemy enemy, Collider2D collider)
        {
            _collider = collider;
            _enemy = enemy;
        }
        public void OnEnter()
        {
            //throw new System.NotImplementedException();
            _collider.enabled = false;
        }

        public void OnExit()
        {
            //throw new System.NotImplementedException();
        }

        public void Tick()
        {
            if(counter >= _enemy.deathCounter)
            {
                _enemy.gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                counter += Time.deltaTime;
            }
            //throw new System.NotImplementedException();
            //Debug.Log("Dead");
        }
    }
}
