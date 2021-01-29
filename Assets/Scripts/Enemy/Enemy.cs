using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.Player;

namespace HMF.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public int health = 50;

        public void TakeDamage(int damage)
        {
            health = Mathf.Max(0, health - damage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();

            player?.TakeDamage();
        }
    }
    
}
