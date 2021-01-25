using HMF.HMFUtilities.DesignPatterns.StatePattern;
using HMF.Player;
using UnityEngine;

namespace HMF
{
    public class AirbornePlayerState : IState
    {
        private PlayerController _playerControler;

        public AirbornePlayerState(PlayerController player)
        {
            _playerControler = player;
        }

        public void OnEnter(){ }

        public void OnExit()
        {
            _playerControler.Jumped = false;
        }

        public void Tick()
        {
            //Debug.Log("Falling");
        }
    }
}
