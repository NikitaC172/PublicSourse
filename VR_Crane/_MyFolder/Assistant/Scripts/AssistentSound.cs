using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AssistentSound : MonoBehaviour
    {
        [SerializeField] private AssistentRenderer _assistentRenderer;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _assistentRenderer.ActionWithCargoChanged += PlaySound;
        }

        private void OnDisable()
        {
            _assistentRenderer.ActionWithCargoChanged += PlaySound;
        }

        private void PlaySound()
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }
        }
    }
}
