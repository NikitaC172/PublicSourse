using System.Collections;
using UnityEngine;

namespace CraneGame
{
    public class AudioplayerAxisMover : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SimpleAxisMover _axisMover;
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
            currentPosition = _axisMover.PositionAxis;

            while (true)
            {
                if (currentPosition != _axisMover.PositionAxis)
                {
                    currentPosition = _axisMover.PositionAxis;
                    _targetVolume = Mathf.Clamp(Mathf.Abs(_axisMover.PositionArm / quaternionCorrector), _minVolume, _maxVolume);
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
