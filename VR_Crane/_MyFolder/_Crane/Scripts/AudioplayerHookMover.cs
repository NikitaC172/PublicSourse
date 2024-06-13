using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraneGame
{
    public class AudioplayerHookMover : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private HookMover _hookMover;
        [SerializeField] private float _deltaTimeVolumeChange = 0.1f;
        [SerializeField] private float _maxVolume = 1.0f;
        [SerializeField] private float _minVolume = 0.0f;

        private float _targetVolume;

        private void OnEnable()
        {
            StartCoroutine(CheckMove());
        }

        private void FixedUpdate()
        {
            if (_audioSource.volume != _targetVolume)
            {
                _audioSource.volume = Mathf.Lerp(_audioSource.volume, _targetVolume, _deltaTimeVolumeChange);
            }
        }

        private IEnumerator CheckMove()
        {
            float currentPosition;
            float quaternionCorrector = 0.07f;
            currentPosition = _hookMover.PositionAxis;

            while (true)
            {
                if (currentPosition != _hookMover.PositionAxis)
                {
                    currentPosition = _hookMover.PositionAxis;
                    _targetVolume = Mathf.Clamp(Mathf.Abs(_hookMover.PositionArm / quaternionCorrector), _minVolume, _maxVolume);
                }
                else
                {
                    _targetVolume = _minVolume;
                }

                yield return null;
            }
        }
    }
}
