using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HMF
{
    public class Blocade : MonoBehaviour
    {
        [SerializeField] private GameObject _blocade = null;
        [SerializeField] private GameObject _audio = null;

        [SerializeField] private GameObject _audio2 = null;
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer == 9)
            {
                _blocade.SetActive(true);
                _audio.SetActive(true);
                _audio2.SetActive(false);
            }
            
        }

        public void Re() 
        {
            _blocade.SetActive(false);
            _audio.SetActive(false);
            _audio2.SetActive(true);
        }
    }
}
