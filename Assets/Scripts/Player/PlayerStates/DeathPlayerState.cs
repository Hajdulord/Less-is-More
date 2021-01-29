using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HMF.HMFUtilities.DesignPatterns.StatePattern;

namespace HMF.Player.PlayerStates
{
    public class DeathPlayerState : IState
    {
        private PlayerController _player;
        public DeathPlayerState(PlayerController controller)
        {
            _player = controller;
        }

        public void OnEnter()
        {
            _player.Revive();
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
