using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HMF
{
    public class End : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio = null;
        [SerializeField] private GameObject _canvas = null;
        void Update()
        {
            if (!_audio.isPlaying)
            {
                _canvas.SetActive(true);
            }
        }
    }
}
