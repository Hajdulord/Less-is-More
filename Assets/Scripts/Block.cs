using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HMF
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private AudioSource _source1 = null;
        [SerializeField] private AudioSource _source2 = null;

        void Update()
        {
            if (!_source1.isPlaying)
            {
                _source2.gameObject.SetActive(true);
                if (!_source2.isPlaying)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
